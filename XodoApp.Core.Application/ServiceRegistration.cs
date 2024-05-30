using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.Services;

namespace XodoApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IDealershipService, DealershipService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
