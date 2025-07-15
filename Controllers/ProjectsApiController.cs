using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;
using SnapSaves.Helpers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsApiController : ControllerBase
{
    private readonly MongoDbContext _dbContext;
    private readonly AppIdentityDbContext _identityDbContext;
    private readonly ProjectHelper _projectHelper;

    public ProjectsApiController(MongoDbContext dbContext, AppIdentityDbContext identityDbContext, ProjectHelper projectHelper)
    {
        _dbContext = dbContext;
        _identityDbContext = identityDbContext;
        _projectHelper = projectHelper;
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] Project updatedProject)
    {
        // Normalize empty UserId to null for anonymous projects
        if (string.IsNullOrWhiteSpace(updatedProject.UserId))
        {
            updatedProject.UserId = null;
            return await SaveAnonymousProject(updatedProject);
        }

        // Existing logged-in user save logic
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId))
            return Unauthorized("You must be logged in.");

        var project = await _dbContext.Projects
            .Find(p => p.Id == updatedProject.Id)
            .FirstOrDefaultAsync();

        if (project == null)
            return NotFound("Project not found.");

        if (project.UserId != currentUserId)
            return Forbid("You do not have permission to modify this project.");

        if (updatedProject.UserId != currentUserId)
            return BadRequest("UserId mismatch.");

        var update = Builders<Project>.Update
            .Set(p => p.Files, updatedProject.Files)
            .Set(p => p.LastModified, DateTime.UtcNow);

        await _dbContext.Projects.UpdateOneAsync(
            p => p.Id == updatedProject.Id,
            update
        );

        return Ok("Project saved successfully.");
    }

    private async Task<IActionResult> SaveAnonymousProject(Project updatedProject)
    {
        // Ensure UserId is null for anonymous projects
        updatedProject.UserId = null;

        // Validate that the project exists and is anonymous
        var project = await _dbContext.Projects
            .Find(p => p.Id == updatedProject.Id && p.UserId == null)
            .FirstOrDefaultAsync();

        if (project == null)
            return NotFound("Anonymous project not found.");

        // Update the anonymous project
        var update = Builders<Project>.Update
            .Set(p => p.Files, updatedProject.Files)
            .Set(p => p.LastModified, DateTime.UtcNow);

        await _dbContext.Projects.UpdateOneAsync(
            p => p.Id == updatedProject.Id,
            update
        );

        // Update the share link's last accessed time if it exists
        var shareLink = await _identityDbContext.ProjectShareLinks
            .FirstOrDefaultAsync(s => s.ProjectMongoId == updatedProject.Id && s.IsAnonymous && s.IsActive);

        if (shareLink != null)
        {
            shareLink.LastAccessedAt = DateTime.UtcNow;
            await _identityDbContext.SaveChangesAsync();
        }

        return Ok("Anonymous project saved successfully.");
    }

    [HttpPost("create-anonymous-sharelink")]
    public async Task<IActionResult> CreateAnonymousShareLink([FromBody] CreateAnonymousShareRequest request)
    {
        try
        {
            // First, try to find the project (it might be anonymous or owned)
            var project = await _dbContext.Projects
                .Find(p => p.Id == request.ProjectId)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return BadRequest("Project not found");
            }

            // If the project belongs to a user, don't allow anonymous share links
            if (!string.IsNullOrEmpty(project.UserId))
            {
                return BadRequest("Cannot create anonymous share link for user-owned project");
            }

            // Check if anonymous share link already exists for this project
            var existingShare = await _identityDbContext.ProjectShareLinks
                .FirstOrDefaultAsync(s => s.ProjectMongoId == request.ProjectId && s.IsAnonymous && s.IsActive);

            if (existingShare != null)
            {
                // Update expiration and last accessed
                existingShare.ExpiresAt = DateTime.UtcNow.AddDays(30);
                existingShare.LastAccessedAt = DateTime.UtcNow;
                await _identityDbContext.SaveChangesAsync();

                var existingShareUrl = Url.Action("ReadOnly", "SnapCode", new { projectId = project.Id, shareToken = existingShare.Token }, Request.Scheme);
                if (existingShareUrl == null)
                {
                    return StatusCode(500, "Failed to generate share URL");
                }

                return Ok(new CreateAnonymousShareResponse
                {
                    Token = existingShare.Token,
                    ProjectId = project.Id,
                    ExpiresAt = existingShare.ExpiresAt.Value,
                    ShareUrl = existingShareUrl
                });
            }

            // Create new anonymous share link
            var shareLink = new ProjectShareLink
            {
                ProjectMongoId = request.ProjectId,
                Token = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                LastAccessedAt = DateTime.UtcNow,
                IsActive = true,
                IsAnonymous = true,
                CreatedByUserId = null,
                TemplateId = request.TemplateId
            };

            _identityDbContext.ProjectShareLinks.Add(shareLink);
            await _identityDbContext.SaveChangesAsync();

            var newShareUrl = Url.Action("ReadOnly", "SnapCode", new { projectId = project.Id, shareToken = shareLink.Token }, Request.Scheme);
            if (newShareUrl == null)
            {
                return StatusCode(500, "Failed to generate share URL");
            }

            return Ok(new CreateAnonymousShareResponse
            {
                Token = shareLink.Token,
                ProjectId = project.Id,
                ExpiresAt = shareLink.ExpiresAt.Value,
                ShareUrl = newShareUrl
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while creating the anonymous share link");
        }
    }

    [HttpPost("update-anonymous-sharelink/{token}")]
    public async Task<IActionResult> UpdateAnonymousShareLink(string token)
    {
        try
        {
            var shareLink = await _identityDbContext.ProjectShareLinks
                .FirstOrDefaultAsync(s => s.Token == token && s.IsAnonymous && s.IsActive);

            if (shareLink == null)
            {
                return NotFound("Share link not found");
            }

            if (shareLink.IsExpired)
            {
                return BadRequest("Share link has expired");
            }

            // Reset expiration to 30 days from now
            shareLink.ExpiresAt = DateTime.UtcNow.AddDays(30);
            shareLink.LastAccessedAt = DateTime.UtcNow;

            await _identityDbContext.SaveChangesAsync();

            return Ok(new { message = "Share link updated successfully", expiresAt = shareLink.ExpiresAt });
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the share link");
        }
    }

    [HttpPost("create-sharelink")]
    public async Task<IActionResult> CreateShareLink([FromBody] CreateShareLinkRequest request)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId) || request.UserId != currentUserId)
            return Forbid("You do not have permission to create a share link for this project.");

        var project = await _dbContext.Projects.Find(p => p.Id == request.ProjectId && p.UserId == currentUserId).FirstOrDefaultAsync();
        if (project == null)
            return NotFound("Project not found or you do not have permission.");

        var token = Guid.NewGuid().ToString("N");

        var shareLink = new ProjectShareLink
        {
            ProjectMongoId = request.ProjectId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            CreatedByUserId = currentUserId
        };
        _identityDbContext.ProjectShareLinks.Add(shareLink);
        await _identityDbContext.SaveChangesAsync();

        var url = Url.Action("ReadOnly", "SnapCode", new { projectId = request.ProjectId, shareToken = token }, Request.Scheme);
        if (url == null)
        {
            return StatusCode(500, "Failed to generate share URL");
        }

        return Ok(new { url });
    }

    [HttpGet("get-sharelink")]
    public async Task<IActionResult> GetShareLink([FromQuery] string projectId)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId))
            return Unauthorized("You must be logged in.");

        var project = await _dbContext.Projects.Find(p => p.Id == projectId && p.UserId == currentUserId).FirstOrDefaultAsync();
        if (project == null)
            return NotFound("Project not found or you do not have permission.");

        var shareLink = await _identityDbContext.ProjectShareLinks
            .Where(l => l.ProjectMongoId == projectId && l.IsActive && !l.IsAnonymous)
            .OrderByDescending(l => l.CreatedAt)
            .FirstOrDefaultAsync();

        if (shareLink == null)
            return NotFound("No share link found.");

        var url = Url.Action("ReadOnly", "SnapCode", new { projectId = projectId, shareToken = shareLink.Token }, Request.Scheme);
        if (url == null)
        {
            return StatusCode(500, "Failed to generate share URL");
        }

        return Ok(new { url });
    }

    [HttpPost("save-a-copy")]
    public async Task<IActionResult> SaveACopy([FromBody] SaveCopyRequest request)
    {
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId) || request.UserId != currentUserId)
            return Forbid("You do not have permission to copy this project.");

        var (success, errorMessage, newProject) = await _projectHelper.CopyProjectAsync(request.ProjectId, request.UserId, request.CourseId);

        if (!success || newProject == null)
            return BadRequest(errorMessage ?? "Failed to copy project.");

        var url = Url.Action(
            "Index",
            "SnapCode",
            new { projectId = newProject.Id, userId = request.UserId, courseId = request.CourseId },
            protocol: Request.Scheme,
            host: Request.Host.ToString()
        );

        return Ok(new
        {
            message = "A copy of your project has been saved.",
            url
        });
    }

    // DTOs
    public class CreateShareLinkRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
    }

    public class SaveCopyRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public int? CourseId { get; set; }
    }

    public class CreateAnonymousShareRequest
    {
        public string ProjectId { get; set; } = string.Empty;
        public string? TemplateId { get; set; }
    }

    public class CreateAnonymousShareResponse
    {
        public string Token { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string ShareUrl { get; set; } = string.Empty;
    }
}