using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Domain.Settings;
using XodoApp.Infrastructure.Shared.Services;

namespace XodoApp.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
