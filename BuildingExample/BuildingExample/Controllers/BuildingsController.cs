using Microsoft.AspNetCore.Mvc;
using BuildingExample.Models;
using BuildingExample.Services;

namespace BuildingExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildings()
        {
            return Ok(await _buildingService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuilding(int id)
        {
            return Ok(await _buildingService.GetOne(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(int id, Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            building = await _buildingService.Update(id, building);

            return Ok(building);
        }

        [HttpPost]
        public async Task<IActionResult> PostBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            building = await _buildingService.Add(building);
            return Created(string.Empty, building);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            await _buildingService.Delete(id);
            return NoContent();
        }
    }
}
