using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TunzWorkout.Application.Commands.Authentication;
using TunzWorkout.Application.Common.Filters;
using TunzWorkout.Application.Common.Services.Authentication;
using TunzWorkout.Application.Common.Services.Equipments;
using TunzWorkout.Application.Common.Services.Exercises;
using TunzWorkout.Application.Common.Services.FitnessProfiles;
using TunzWorkout.Application.Common.Services.Goals;
using TunzWorkout.Application.Common.Services.Levels;
using TunzWorkout.Application.Common.Services.Muscles;
using TunzWorkout.Application.Common.Services.Rounds;
using TunzWorkout.Application.Common.Services.Users;
using TunzWorkout.Application.Common.Services.Wishlists;
using TunzWorkout.Application.Common.Services.Workouts;
using TunzWorkout.Application.Common.Validators.Equipments;
using TunzWorkout.Application.Common.Validators.Exercises;
using TunzWorkout.Application.Common.Validators.FitnessProfiles;
using TunzWorkout.Application.Common.Validators.Goals;
using TunzWorkout.Application.Common.Validators.Levels;
using TunzWorkout.Application.Common.Validators.Muscles;
using TunzWorkout.Application.Common.Validators.RoundExercises;
using TunzWorkout.Application.Common.Validators.Rounds;
using TunzWorkout.Application.Common.Validators.Users;
using TunzWorkout.Application.Common.Validators.Workouts;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.Goals;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.Muscles;
using TunzWorkout.Domain.Entities.RoundExercises;
using TunzWorkout.Domain.Entities.Rounds;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFitnessProfileService, FitnessProfileService>();
            services.AddScoped<IMuscleService, MuscleService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IValidator<Level>, LevelValidator>();
            services.AddScoped<IValidator<Muscle>, MuscleValidator>();
            services.AddScoped<IValidator<Equipment>, EquipmentValidator>();
            services.AddScoped<IValidator<Exercise>, ExerciseValidator>();
            services.AddScoped<IValidator<GetAllExercisesOptions>, GetAllExercisesOptionsValidator>();
            services.AddScoped<IValidator<User>, UserValidator>();
            services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
            services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
            services.AddScoped<IValidator<Goal>, GoalValidator>();
            services.AddScoped<IValidator<FitnessProfile>, FitnessProfileValidator>();
            services.AddScoped<IValidator<Workout>, WorkoutValidator>();
            services.AddScoped<IValidator<Round>, RoundValidator>();
            services.AddScoped<IValidator<RoundExercise>, RoundExerciseValidator>();

            return services;
        }
    }
}
