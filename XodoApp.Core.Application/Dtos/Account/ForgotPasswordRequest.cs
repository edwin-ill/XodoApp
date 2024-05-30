using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XodoApp.Core.Application.Dtos.Account
{
    /// <summary>
    /// Parámetros para iniciar el proceso de recuperar la contrasenia
    /// </summary> 
    public class ForgotPasswordRequest
    {
        [SwaggerParameter(Description = "Correo del usuario que olvido su contraseña")]
        public string Email { get; set; }
    }
}
