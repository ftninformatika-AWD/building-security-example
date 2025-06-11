using Microsoft.AspNetCore.Mvc;
using BuildingExample.Services;
using BuildingExample.DTOs;
using BuildingExample.Validators;

namespace BuildingExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetApartments()
        {
            return Ok(await _apartmentService.GetAll());
        }

        [HttpGet("custom")]
        public async Task<IActionResult> GetCustomApartments()
        {
            return Ok(await _apartmentService.GetAllCustom());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartment(int id)
        {
            return Ok(await _apartmentService.GetOne(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, ApartmentUpdateDTO apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var savedApartment = await _apartmentService.Update(id, apartment);
            return Ok(savedApartment);
        }

        [HttpPost]
        public async Task<IActionResult> PostApartment(ApartmentCreateDTO apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var savedApartment = await _apartmentService.Add(apartment);
            return Created(string.Empty, savedApartment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            await _apartmentService.Delete(id);
            return NoContent();
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchApartments(ApartmentSearchDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            ApartmentValidator.ValidateSearchApartment(dto);
            return Ok(await _apartmentService.SearchByArea(dto.AreaFrom, dto.AreaTo));
        }
    }
}
