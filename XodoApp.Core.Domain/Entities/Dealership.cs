using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Domain.Common;

namespace XodoApp.Core.Domain.Entities
{
    public class Dealership : AuditableBaseEntity
    {
        public string Name { get; set; } // Name of the dealership
        public string Address { get; set; } // Physical address
        public string City { get; set; } // City location
        public string PhoneNumber { get; set; } // Contact phone number
        public string Email { get; set; } // Contact email
        public List<Vehicle>? Vehicles { get; set; } // List of vehicles available at the dealership
    }
}
