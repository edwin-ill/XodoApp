using AutoMapper;
using Microsoft.AspNetCore.Http;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Helpers;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.ViewModels.Vehicles;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Services
{
    public class VehicleService : GenericService<SaveVehicleViewModel, VehicleViewModel, Vehicle>, IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(vehicleRepository, mapper)
        {
            _vehicleRepository = vehicleRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
    }
}
