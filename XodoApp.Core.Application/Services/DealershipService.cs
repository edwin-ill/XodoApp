using AutoMapper;
using Microsoft.AspNetCore.Http;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Helpers;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.ViewModels.Dealerships;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Services
{
    public class DealershipService : GenericService<SaveDealershipViewModel, DealershipViewModel, Dealership>, IDealershipService
    {
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public DealershipService(IDealershipRepository dealershipRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(dealershipRepository, mapper)
        {
            _dealershipRepository = dealershipRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
    }
}
