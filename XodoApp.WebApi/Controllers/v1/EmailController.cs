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
                Subject = "New client inquiry",
                To = "tamoaqui5@gmail.com",
                Body = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Consulta de nuevo cliente</title>
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
                            <h1>Consulta de nuevo cliente</h1>
                        </div>
                        <div class=""content"">
                            <p>Un cliente potencial ha enviado una consulta a través del formulario de contacto. Revise los detalles a continuación y responda con prontitud.</p>
                            <h2>Información del cliente:</h2>
                            <p><strong>Nombre:</strong> {command.SenderName}</p>
                            <p><strong>Email:</strong> {command.SenderEmail}</p>
                            <h2>Message:</h2>
                            <p><strong>Auto:</strong> {command.Car}</p>
                            <p>{command.Message}</p>
                        </div>
                        <div class=""footer"">
                            <p>Este es un mensaje automatizado del sistema de contacto de la aplicación Xodo App Import. Por favor no responder a este email.</p>
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
