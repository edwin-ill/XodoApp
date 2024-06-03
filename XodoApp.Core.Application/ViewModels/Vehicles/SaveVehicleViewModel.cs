using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using XodoApp.Core.Application.ViewModels.Dealerships;

namespace XodoApp.Core.Application.ViewModels.Vehicles
{
    public class SaveVehicleViewModel
    {
        public int Id { get; set; }
        public string? VIN { get; set; }
        [Required(ErrorMessage = "El campo marca es obligatorio.")]
        public string CarMake { get; set; }
        [Required(ErrorMessage = "El campo modelo es obligatorio.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "El campo año es obligatorio.")]
        public int Year { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public string? EngineType { get; set; }
        [Required(ErrorMessage = "El campo tipo de transmisión es obligatorio.")]
        public string TransmissionType { get; set; }
        public int? Mileage { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El campo dealer es obligatorio.")]
        public int DealershipId { get; set; }
        [Required(ErrorMessage = "El campo tipo de vehículo es obligatorio.")]
        public string VehicleType { get; set; }
        [DataType(DataType.Upload)]
        public IFormFileCollection Files { get; set; }

        public List<DealershipViewModel>? Dealerships { get; set; }


    }
}
