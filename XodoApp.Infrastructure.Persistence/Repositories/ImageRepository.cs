using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Domain.Entities;
using XodoApp.Infrastructure.Persistence.Contexts;

namespace XodoApp.Infrastructure.Persistence.Repositories
{
    public class ImageRepository : GenericRepository<VehicleImage>, IImageRepository
    {
        private readonly ApplicationContext _dbContext;

        public ImageRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
