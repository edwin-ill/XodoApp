using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using XodoApp.Core.Application.Dtos.Email;
using XodoApp.Core.Application.Features.Dealerships.Commands.CreateDealership;
using XodoApp.Core.Application.Interfaces.Services;

namespace XodoApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de mensajes por email")]
    public class EmailController : BaseApiController
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;          
        }

            [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de mensajes por email",
            Description = "Recibe los parametros necesarios para crear un mensaje y mandarlo por email")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post([FromBody] EmailRequest command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            
            var formattedEmail = new EmailRequest()
            {
                Subject = command.Subject,
                To = command.To,
                Body = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>New Client Inquiry</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                line-height: 1.6;
                                color: #333;
                                max-width: 600px;
                                margin: 0 auto;
                                padding: 20px;
                            }}
                            .header {{
                                background-color: #003366;
                                color: white;
                                padding: 20px;
                                text-align: center;
                            }}
                            .content {{
                                background-color: #f9f9f9;
                                border: 1px solid #ddd;
                                padding: 20px;
                                margin-top: 20px;
                            }}
                            .footer {{
                                text-align: center;
                                margin-top: 20px;
                                font-size: 0.8em;
                                color: #666;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>New Client Inquiry</h1>
                        </div>
                        <div class=""content"">
                            <p>A potential client has submitted an inquiry through the ""Xodo AutoImport"" form. Please review the details below and respond promptly.</p>
                            <h2>Client Information:</h2>
                            <p><strong>Name:</strong> {command.SenderName}</p>
                            <p><strong>Email:</strong> {command.SenderEmail}</p>
                            <h2>Message:</h2>
                            <p>{command.Message}</p>
                        </div>
                        <div class=""footer"">
                            <p>This is an automated message from the Xodo App contact system. Please do not reply to this email.</p>
                        </div>
                    </body>
                    </html>",
                From = command.From

            };
            await _emailService.SendAsync(formattedEmail);

            return NoContent();


        }
    }
}
