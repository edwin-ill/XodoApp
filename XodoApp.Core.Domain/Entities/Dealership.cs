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
        public string Name { get; set; } 
        public string Address { get; set; } 
        public string City { get; set; } 
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
    }
}
