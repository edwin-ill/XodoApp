using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.ViewModels.Vehicles;

namespace XodoApp.Core.Application.ViewModels.Dealerships
{
    public class DealershipViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } 
        public string City { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Email { get; set; } 
        public List<VehicleViewModel>? Vehicles { get; set; }
    }
}
