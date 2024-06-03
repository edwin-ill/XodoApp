﻿using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Interfaces.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<VehicleImage> AddImage(VehicleImage entity);
        Task<List<Vehicle>> GetAllWithImagesAsync(); 
    }
}
