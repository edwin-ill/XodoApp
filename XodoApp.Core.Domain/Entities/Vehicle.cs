using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Domain.Common;
using XodoApp.Core.Domain.Enums;

namespace XodoApp.Core.Domain.Entities
{
    public class Vehicle : AuditableBaseEntity
    {
        public string? VIN { get; set; } // Vehicle Identification Number
        public CarMake CarMake { get; set; } // Brand of the vehicle, e.g., Ford, Toyota
        public string Model { get; set; } // Model of the vehicle, e.g., Mustang, Camry
        public int Year { get; set; } // Year of manufacture
        public string? Color { get; set; } // Color of the vehicle
        public decimal? Price { get; set; } // Sale price of the vehicle
        public string? EngineType { get; set; } // e.g., V6, Electric
        public TransmissionType TransmissionType { get; set; } // e.g., Automatic, Manual
        public int? Mileage { get; set; } // Mileage of the vehicle
        public string Description { get; set; } // Description of the vehicle
        public string ImageUrl { get; set; } // URL to an image of the vehicle
        public int DealershipId  { get; set; }
        public Dealership Dealership { get; set; } // Navigation property to Dealership
        public VehicleType VehicleType { get; set; }
    }
}
 