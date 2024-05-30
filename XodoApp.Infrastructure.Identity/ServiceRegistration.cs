using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Infrastructure.Identity.Contexts;
using XodoApp.Infrastructure.Identity.Entities;
using XodoApp.Infrastructure.Identity.Services;

namespace XodoApp.Infrastructure.Identity
{
    public static class ServiceRegistration
    {

        public static void AddIdentityInfrastructureForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            ContextConfiguration(services, configuration);

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.AddAuthentication();
            #endregion

            ServiceConfiguration(services);
        }

        #region "Private methods"

        private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion
        }

        private static void ServiceConfiguration(IServiceCollection services)
        {
            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }
        #endregion
    }
}


