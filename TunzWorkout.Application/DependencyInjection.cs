using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TunzWorkout.Application.Common.Services.Equipments;
using TunzWorkout.Application.Common.Services.Exercises;
using TunzWorkout.Application.Common.Services.Levels;
using TunzWorkout.Application.Common.Services.Muscles;
using TunzWorkout.Application.Common.Validators.Equipments;
using TunzWorkout.Application.Common.Validators.Exercises;
using TunzWorkout.Application.Common.Validators.Levels;
using TunzWorkout.Application.Common.Validators.Muscles;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMuscleService, MuscleService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IValidator<Level>, LevelValidator>();
            services.AddScoped<IValidator<Muscle>, MuscleValidator>();
            services.AddScoped<IValidator<Equipment>, EquipmentValidator>();
            services.AddScoped<IValidator<Exercise>, ExerciseValidator>();

            return services;
        }
    }
}
