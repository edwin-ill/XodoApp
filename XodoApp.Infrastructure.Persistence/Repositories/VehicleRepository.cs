using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Domain.Entities;
using XodoApp.Infrastructure.Persistence.Contexts;

namespace XodoApp.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationContext _dbContext;

        public VehicleRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<Vehicle>> GetAllAsync()
        {
            var vehicleList = await _dbContext.Set<Vehicle>().Include(v => v.Dealership).ToListAsync();
            return vehicleList;
        }

        public async Task<List<Vehicle>> GetAllWithImagesAsync()
        {
            var vehicleList = await _dbContext.Set<Vehicle>().Include(v => v.Dealership).Include(v => v.VehicleImages).ToListAsync();
            return vehicleList;
        }


        public async Task<VehicleImage> AddImage(VehicleImage entity)
        {
            await _dbContext.Set<VehicleImage>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
