using System.ComponentModel.DataAnnotations;

namespace XodoApp.Core.Application.ViewModels.Dealerships
{
    public class SaveDealershipViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo ciudad es obligatorio.")]
        public string City { get; set; }
        [Required(ErrorMessage = "El campo teléfono es obligatorio.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        public string Email { get; set; }

    }
}
