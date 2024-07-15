using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.ViewModels.Dealerships;
using XodoApp.Core.Application.ViewModels.Vehicles;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Interfaces.Services
{
    public interface IImageService : IGenericService<SaveVehicleImageViewModel, VehicleImageViewModel, VehicleImage>
    {
    }
}
