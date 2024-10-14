using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dto.Equipments;
using TunzWorkout.Application.Common.Services.Equipments;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentRequest request)
        {
            if (request is null)
            {
                return BadRequest("Invalid request: Muscle data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var equipment = request.MapToEquipment();
            await _equipmentService.CreateAsync(equipment);
            var response = equipment.MapToResponse();
            return CreatedAtAction(nameof(CreateEquipment), new {id = equipment.Id}, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEquipments()
        {
            var equipments = await _equipmentService.GetAllAsync();
            var response = new List<EquipmentResponse>();

            foreach(var equipment in equipments)
            {
                response.Add(equipment.MapToResponse());
            }
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] Guid id)
        {
            var equipment = await _equipmentService.EquipmentByIdAsync(id);
            if (equipment is null)
            {
                return NotFound();
            }
            var response = equipment.MapToResponse();
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEquipments([FromForm] UpdateEquipmentRequest request, [FromRoute] Guid id)
        {
            var equipment = request.MapToEquipment(id);

            await _equipmentService.UpdateAsync(equipment);

            var response = equipment.MapToResponse();
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveMuscles([FromRoute] Guid id)
        {
            var deleted = await _equipmentService.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
