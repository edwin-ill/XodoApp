﻿using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XodoApp.Core.Application.Dtos.Account

{
    /// <summary>
    /// Parámetros para la creacion de usuario basico
    /// </summary> 
    public class RegisterRequest
    {
        [SwaggerParameter(Description = "El nombre de el usuario")]
        public string FirstName { get; set; }
        [SwaggerParameter(Description = "El apellido de el usuario")]
        public string LastName { get; set; }
        [SwaggerParameter(Description = "El correo de el usuario")]
        public string Email { get; set; }
        [SwaggerParameter(Description = "El nombre de usuario")]
        public string UserName { get; set; }
        [SwaggerParameter(Description = "La contraseña del usuario")]
        public string Password { get; set; }
        [SwaggerParameter(Description = "La confirmacion de la contraseña del usuario")]
        public string ConfirmPassword { get; set; }
        [SwaggerParameter(Description = "El telefono del usuario")]
        public string Phone { get; set; }
        [SwaggerParameter(Description = "La imagen del usuario")]
        public string? img { get; set; }
    }
}
