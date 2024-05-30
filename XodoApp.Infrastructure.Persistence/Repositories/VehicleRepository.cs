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
    }
}
