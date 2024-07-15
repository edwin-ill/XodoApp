using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XodoApp.Core.Application.Dtos.Vehicle
{
    public class VehicleImageDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
    }
}
