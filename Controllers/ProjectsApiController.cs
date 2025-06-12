using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SnapSaves.Models;

[ApiController]
[Route("api/[controller]")]
public class ProjectsApiController : ControllerBase
{
    private readonly MongoDbContext _dbContext;

    public ProjectsApiController(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
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
}