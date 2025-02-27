using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Infrastructure.Data;
using TunzWorkout.Infrastructure.Files;
using TunzWorkout.Infrastructure.Repository;

namespace TunzWorkout.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserGoalRepository, UserGoalRepository>();
            services.AddScoped<IMuscleRepository, MuscleRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IVideoFileService, VideoFileService>();
            services.AddScoped<IExerciseEquipmentRepository, ExerciseEquipmentRepository>();
            services.AddScoped<IExerciseMuscleRepository, ExerciseMuscleRepository>();
            services.AddScoped<IMuscleImageRepository, MuscleImageRepository>();
            services.AddScoped<IEquipmentImageRepository, EquipmentImageRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IFitnessProfileRepository, FitnessProfileRepository>();
            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IRoundExerciseRepository, RoundExerciseRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
