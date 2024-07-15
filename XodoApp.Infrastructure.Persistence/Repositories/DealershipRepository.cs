using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Domain.Entities;
using XodoApp.Infrastructure.Persistence.Contexts;

namespace XodoApp.Infrastructure.Persistence.Repositories
{
    public class DealershipRepository : GenericRepository<Dealership>, IDealershipRepository
    {
        private readonly ApplicationContext _dbContext;

        public DealershipRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
