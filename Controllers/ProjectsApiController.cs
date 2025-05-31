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

        Console.WriteLine($"SaveProject: Received project with ID {updatedProject.Id}, Name {updatedProject.Name}, {updatedProject.Files?.Count ?? 0} files.");

        var filter = Builders<Project>.Filter.Eq(p => p.Id, updatedProject.Id);
        var updateResult = await _dbContext.Projects.ReplaceOneAsync(filter, updatedProject);

        if (updateResult.MatchedCount == 0)
        {
            Console.WriteLine($"SaveProject: Project with ID {updatedProject.Id} not found.");
            return NotFound("Project not found.");
        }

        Console.WriteLine($"SaveProject: Project with ID {updatedProject.Id} updated successfully.");
        return Ok(new { success = true });
    }
}