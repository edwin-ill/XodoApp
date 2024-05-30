using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XodoApp.Core.Application.Dtos.Account
{
    /// <summary>
    /// Par�metros para realizar la autenticacion del usuario
    /// </summary> 
    public class AuthenticationRequest
    {
        [SwaggerParameter(Description = "Correo del usuario que desea iniciar sesion")]
        public string Email { get; set; }
        [SwaggerParameter(Description = "Contrase�a del usuario que desea iniciar sesion")]
        public string Password { get; set; }
    }
}
