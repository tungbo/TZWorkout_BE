using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dto.Muscles;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MusclesController(IMuscleRepository muscleRepository, IUnitOfWork unitOfWork)
        {
            _muscleRepository = muscleRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMuscle(MuscleDTO muscleDTO)
        {
            if (muscleDTO is null)
            {
                return Problem(detail: "Muscle is null", statusCode: 400, title: "Error");
            }
            var muscle = muscleDTO.MapToMuscle();
            await _muscleRepository.CreateAsync(muscle);
            await _unitOfWork.CommitChangesAsync();

            var response = muscle.MapToMuscleDTO();

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMuscles()
        {
            var muscles = await _muscleRepository.GetAllAsync();

            var response = new List<MuscleDTO>();

            foreach(var muscle in muscles)
            {
                response.Add(muscle.MapToResponse());
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMuscles(MuscleDTO muscleDTO, Guid id)
        {
            var muscles = muscleDTO.MapToMuscle(id);
            
            await _muscleRepository.UpdateAsync(muscles);
            await _unitOfWork.CommitChangesAsync();

            var response = muscles.MapToResponse();
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveMuscles(Guid id)
        {
            if(!await _muscleRepository.ExistByIdAsync(id))
            {
                return Problem(detail: "Not Found", statusCode: 400, title: "Error");
            }

            await _muscleRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommitChangesAsync();

            return Ok();
        }
    }
}
