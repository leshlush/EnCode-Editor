using Microsoft.AspNetCore.Mvc;
using SnapSaves.Data;
using SnapSaves.Helpers;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningPathApiController : ControllerBase
    {
        private readonly LearningPathHelper _learningPathHelper;

        public LearningPathApiController(AppIdentityDbContext context)
        {
            _learningPathHelper = new LearningPathHelper(context);
        }

        // POST: api/LearningPathApi/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateLearningPath([FromBody] CreateLearningPathRequest request)
        {
            try
            {
                var learningPath = await _learningPathHelper.CreateLearningPathAsync(request.Name, request.Description);
                return Ok(new { learningPath.Id, learningPath.Name });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/LearningPathApi/AddLearningItems
        [HttpPost("AddLearningItems")]
        public async Task<IActionResult> AddLearningItems([FromBody] AddLearningItemsRequest request)
        {
            try
            {
                await _learningPathHelper.AddLearningItemsAsync(request.LearningPathId, request.LearningItems);
                return Ok("LearningItems added successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/LearningPathApi/AddUnits
        [HttpPost("AddUnits")]
        public async Task<IActionResult> AddUnits([FromBody] AddUnitsRequest request)
        {
            try
            {
                await _learningPathHelper.AddUnitsAsync(request.LearningPathId, request.Units);
                return Ok("Units added successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

    // Request models
    public class CreateLearningPathRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class AddLearningItemsRequest
    {
        public int LearningPathId { get; set; }
        public List<LearningItemRequest> LearningItems { get; set; } = new List<LearningItemRequest>();
    }

    public class AddUnitsRequest
    {
        public int LearningPathId { get; set; }
        public List<UnitRequest> Units { get; set; } = new List<UnitRequest>();
    }
}