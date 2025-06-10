using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SnapSaves.Data;
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
    public async Task<IActionResult> SaveProject([FromBody] Project updatedProject)
    {
        if (updatedProject == null || string.IsNullOrEmpty(updatedProject.Id))
        {
            Console.WriteLine("SaveProject: Invalid project data received.");
            return BadRequest("Invalid project data.");
        }

        var filter = Builders<Project>.Filter.Eq(p => p.Id, updatedProject.Id);

        // Build the update definition
        var updateDef = Builders<Project>.Update
            .Set(p => p.Name, updatedProject.Name)
            .Set(p => p.Files, updatedProject.Files)
            .Set(p => p.LastModified, DateTime.UtcNow);

        // Only update InstructionsId if it is not null (explicitly set)
        if (updatedProject.InstructionsId != null)
            updateDef = updateDef.Set(p => p.InstructionsId, updatedProject.InstructionsId);

        var updateResult = await _dbContext.Projects.UpdateOneAsync(filter, updateDef);

        if (updateResult.MatchedCount == 0)
        {
            Console.WriteLine($"SaveProject: Project with ID {updatedProject.Id} not found.");
            return NotFound("Project not found.");
        }

        Console.WriteLine($"SaveProject: Project with ID {updatedProject.Id} updated successfully.");
        return Ok(new { success = true });
    }
}