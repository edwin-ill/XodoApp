using XodoApp.Core.Application.ViewModels.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace XodoApp.Core.Application.Dtos.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un telefono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Debe colocar el tipo de cuenta")]
        [DataType(DataType.Text)]
        public List<string> Role { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? img {  get; set; }
        public bool EmailConfirmed { get; set; }
        public List<VehicleViewModel>? Vehicles { get; set; }

    }
}
