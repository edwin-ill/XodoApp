using XodoApp.Core.Application.ViewModels.Vehicles;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Interfaces.Services
{
    public interface IVehicleService : IGenericService<SaveVehicleViewModel, VehicleViewModel, Vehicle>
    {
    }
}
