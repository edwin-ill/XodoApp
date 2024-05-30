using XodoApp.Core.Application.Dtos.Email;
using XodoApp.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XodoApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
