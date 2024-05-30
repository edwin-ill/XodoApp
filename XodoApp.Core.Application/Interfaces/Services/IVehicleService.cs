using XodoApp.Core.Application.ViewModels.Dealerships;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Interfaces.Services
{
    public interface IDealershipService : IGenericService<SaveDealershipViewModel, DealershipViewModel, Dealership>
    {
    }
}
