using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.ViewModels.Dealerships;

namespace XodoApp.Core.Application.ViewModels.Vehicles
{
    public class VehicleViewModel
    {
        public string? VIN { get; set; } 
        public string CarMake { get; set; } 
        public string Model { get; set; }
        public int Year { get; set; } 
        public string? Color { get; set; }
        public decimal? Price { get; set; }
        public string? EngineType { get; set; } 
        public string TransmissionType { get; set; }
        public int? Mileage { get; set; }
        public string Description { get; set; } 
        public string ImageUrl { get; set; } 
        public int DealershipId { get; set; }
        public DealershipViewModel Dealership { get; set; }
        public string VehicleType { get; set; }
    }
}
