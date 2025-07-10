using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SnapSaves.Data;
using SnapSaves.Models;

[ApiController]
[Route("api/[controller]")]
public class ProjectsApiController : ControllerBase
{
    private readonly MongoDbContext _dbContext;
    private readonly AppIdentityDbContext _identityDbContext;

    public ProjectsApiController(MongoDbContext dbContext, AppIdentityDbContext identityDbContext)
    {
        _dbContext = dbContext;
        _identityDbContext = identityDbContext;
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] Project updatedProject)
    {
        // 1. Get the current user's MongoUserId from claims
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId))
            return Unauthorized("You must be logged in.");

        // 2. Fetch the project from MongoDB by ID
        var project = await _dbContext.Projects
            .Find(p => p.Id == updatedProject.Id)
            .FirstOrDefaultAsync();

        if (project == null)
            return NotFound("Project not found.");

        // 3. Verify ownership
        if (project.UserId != currentUserId)
            return Forbid("You do not have permission to modify this project.");

        // 4. (Optional) Double-check the incoming payload's UserId matches the claim
        if (updatedProject.UserId != currentUserId)
            return BadRequest("UserId mismatch.");

        // 5. Update the project (only allow fields you want to update)
        // For example, update files and last modified:
        var update = Builders<Project>.Update
            .Set(p => p.Files, updatedProject.Files)
            .Set(p => p.LastModified, DateTime.UtcNow);

        await _dbContext.Projects.UpdateOneAsync(
            p => p.Id == updatedProject.Id,
            update
        );

        return Ok("Project saved successfully.");
    }

    [HttpPost("create-sharelink")]
    public async Task<IActionResult> CreateShareLink([FromBody] CreateShareLinkRequest request)
    {
        // Validate user
        var currentUserId = User.Claims.FirstOrDefault(c => c.Type == "MongoUserId")?.Value;
        if (string.IsNullOrEmpty(currentUserId) || request.UserId != currentUserId)
            return Forbid("You do not have permission to create a share link for this project.");

        // Validate project ownership
        var project = await _dbContext.Projects.Find(p => p.Id == request.ProjectId && p.UserId == currentUserId).FirstOrDefaultAsync();
        if (project == null)
            return NotFound("Project not found or you do not have permission.");

        // Generate a secure token
        var token = Guid.NewGuid().ToString("N");

        // Create and save the share link
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

        // Build the shareable URL (SnapCode ReadOnly action)
        var url = Url.Action("ReadOnly", "SnapCode", new { projectId = request.ProjectId, shareToken = token }, Request.Scheme);

        return Ok(new { url });
    }

    // DTO for request
    public class CreateShareLinkRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
    }
}