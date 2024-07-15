using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Infrastructure.Persistence.Contexts;
using XodoApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace XodoApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IDealershipRepository, DealershipRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            #endregion
        }
    }
}
