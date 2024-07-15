using AutoMapper;
using Microsoft.AspNetCore.Http;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.ViewModels.Vehicles;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Services
{
    public class ImageService : GenericService<SaveVehicleImageViewModel, VehicleImageViewModel, VehicleImage>, IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(imageRepository, mapper)
        {
            _imageRepository = imageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }
}
