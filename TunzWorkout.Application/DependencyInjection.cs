using Microsoft.Extensions.DependencyInjection;
using TunzWorkout.Application.Common.Services.Equipments;
using TunzWorkout.Application.Common.Services.Muscles;

namespace TunzWorkout.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMuscleService, MuscleService>();
            services.AddScoped<IEquipmentService, EquipmentService>();

            return services;
        }
    }
}
