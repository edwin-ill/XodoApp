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
        public string? VIN { get; set; } 
        public CarMake CarMake { get; set; } 
        public string Model { get; set; } 
        public int Year { get; set; } 
        public string? Color { get; set; } 
        public decimal? Price { get; set; }
        public string? EngineType { get; set; } 
        public TransmissionType TransmissionType { get; set; } 
        public int? Mileage { get; set; }
        public string Description { get; set; } 
        public int DealershipId  { get; set; }
        public Dealership Dealership { get; set; } 
        public VehicleType VehicleType { get; set; }
        public List<VehicleImage> VehicleImages { get; set; }
    }
}
 