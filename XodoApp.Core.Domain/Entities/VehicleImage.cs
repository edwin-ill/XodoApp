using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Domain.Common;

namespace XodoApp.Core.Domain.Entities
{
    public class VehicleImage : AuditableBaseEntity
    {
        public int VehicleId { get; set; }
        public string ImageUrl { get; set; }
        public Vehicle Vehicle { get; set; }
        public string PublicId { get; set; }
    }
}
