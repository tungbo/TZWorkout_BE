using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Equipments;
using TunzWorkout.Application.Common.Errors;
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
            var toEquipment = request.MapToEquipment();
            var result = await _equipmentService.CreateAsync(toEquipment);
            return result.Match(
                equipment => CreatedAtAction(nameof(GetEquipmentById), new { id = equipment.Id }, equipment.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEquipments()
        {
            var result = await _equipmentService.GetAllAsync();

            return result.Match(
                equipments => Ok(equipments.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] Guid id)
        {
            var result = await _equipmentService.EquipmentByIdAsync(id);
            return result.Match(
                equipment => Ok(equipment.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEquipments([FromForm] UpdateEquipmentRequest request, [FromRoute] Guid id)
        {
            var equipment = request.MapToEquipment(id);

            var result = await _equipmentService.UpdateAsync(equipment);

            return result.Match(equipment => Ok(equipment.MapToResponse()), errors => ErrorHandlingExtensions.HandleError(errors));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveMuscles([FromRoute] Guid id)
        {
            var result = await _equipmentService.DeleteByIdAsync(id);
            return result.Match(_ => NoContent(), errors => ErrorHandlingExtensions.HandleError(errors));
        }

    }
}
