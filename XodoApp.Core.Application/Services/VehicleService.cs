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
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IDealershipRepository dealershipRepository) : base(vehicleRepository, mapper)
        {
            _vehicleRepository = vehicleRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _dealershipRepository = dealershipRepository;
        }

        public async Task<SaveVehicleImageViewModel> AddImage(SaveVehicleImageViewModel vm)
        {
            VehicleImage entity = _mapper.Map<VehicleImage>(vm);

            entity = await _vehicleRepository.AddImage(entity);

            SaveVehicleImageViewModel entityVm = _mapper.Map<SaveVehicleImageViewModel>(entity);

            return entityVm;
        }      

        public async Task<List<VehicleViewModel>> GetAllViewModelWithImages()
        {
            var entityList = await _vehicleRepository.GetAllWithImagesAsync();
            var vm = _mapper.Map<List<VehicleViewModel>>(entityList);
            return vm;
        }

    }
}
