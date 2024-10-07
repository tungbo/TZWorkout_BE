using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TunzWorkout.Infrastructure.Database;

namespace TunzWorkout.Infrastructure
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=LAPTOP-KQUEQHAL\\TUNZ;Database=TunzWorkout;Trusted_Connection=true;MultipleActiveResultSets=true");
            });
            return services;
        }
    }
}
