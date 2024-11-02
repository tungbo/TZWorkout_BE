using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Equipments;
using TunzWorkout.Application.Common.Services.Equipments;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class EquipmentsController : ApiController
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        [RequestSizeLimit(1 * 1024 * 1024)]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentRequest request)
        {
            var toEquipment = request.MapToEquipment();
            var result = await _equipmentService.CreateAsync(toEquipment);
            return result.Match(equipment => CreatedAtAction(nameof(GetEquipmentById), new { id = equipment.Id }, equipment.MapToResponse()), Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEquipments()
        {
            var result = await _equipmentService.GetAllAsync();

            return result.Match(equipments => Ok(equipments.MapToResponse()), Problem);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] Guid id)
        {
            var result = await _equipmentService.EquipmentByIdAsync(id);
            return result.Match(equipment => Ok(equipment.MapToResponse()), Problem);
        }

        [HttpPut("{id:guid}")]
        [RequestSizeLimit(1 * 1024 * 1024)]
        public async Task<IActionResult> UpdateEquipments([FromForm] UpdateEquipmentRequest request, [FromRoute] Guid id)
        {
            var equipment = request.MapToEquipment(id);

            var result = await _equipmentService.UpdateAsync(equipment);

            return result.Match(equipment => Ok(equipment.MapToResponse()), Problem);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveMuscles([FromRoute] Guid id)
        {
            var result = await _equipmentService.DeleteByIdAsync(id);
            return result.Match(_ => NoContent(), Problem);
        }

    }
}
