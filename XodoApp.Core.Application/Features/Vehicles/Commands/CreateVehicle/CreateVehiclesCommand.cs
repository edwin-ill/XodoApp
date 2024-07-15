using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using XodoApp.Core.Application.Features.Vehicles.Commands.UpdateVehicle;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.ViewModels.Vehicles;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle
{   
    public class CreateVehiclesCommand :IRequest<Response<VehicleCreateResponse>>
    {       
        [SwaggerParameter(Description = "El VIN del vehículo")]
        public string? VIN { get; set; }
        [SwaggerParameter(Description = "La marca del vehículo")]
        public string CarMake { get; set; }
        [SwaggerParameter(Description = "El modelo del vehículo")]
        public string Model { get; set; }
        [SwaggerParameter(Description = "El año del vehículo")]
        public int Year { get; set; }
        [SwaggerParameter(Description = "El color del vehículo")]
        public string? Color { get; set; }
        [SwaggerParameter(Description = "El precio del vehículo")]
        public decimal? Price { get; set; }
        [SwaggerParameter(Description = "El tipo de motor del vehículo")]
        public string? EngineType { get; set; }
        [SwaggerParameter(Description = "El tipo de transmisión del vehículo")]
        public string TransmissionType { get; set; }
        [SwaggerParameter(Description = "El millaje del vehículo")]
        public int? Mileage { get; set; }
        [SwaggerParameter(Description = "La descripción del vehículo")]
        public string Description { get; set; }
        [SwaggerParameter(Description = "El id del dealer del vehículo")]
        public int DealershipId { get; set; }
        [SwaggerParameter(Description = "El tipo de vehículo")]
        public string VehicleType { get; set; }      

        public class CreateVehiclesCommandHandler : IRequestHandler<CreateVehiclesCommand, Response<VehicleCreateResponse>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IImageService _imageService;
            private readonly IVehicleService _vehicleService;
            private readonly IMapper _mapper;

            public CreateVehiclesCommandHandler(IVehicleRepository vehicleRepository, IVehicleService vehicleService, IMapper mapper, IImageService imageService)
            {
                _vehicleRepository = vehicleRepository;
                _vehicleService = vehicleService;
                _mapper = mapper;
                _imageService = imageService;
            }

            public async Task<Response<VehicleCreateResponse>> Handle(CreateVehiclesCommand command, CancellationToken cancellationToken)
            {
                var vehicle = _mapper.Map<SaveVehicleViewModel>(command);
                var vehicleVm = await _vehicleService.Add(vehicle);
                var vehicleResponse = _mapper.Map<VehicleCreateResponse>(vehicleVm);
                return new Response<VehicleCreateResponse>(vehicleResponse);
            }

            private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
            {

                if (isEditMode)
                {
                    if (file == null)
                    {
                        return imagePath;
                    }
                }

                string basePath = $"/Images/Vehicles/{id}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

                //create folder if not exist
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //get file extension
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new(file.FileName);
                string fileName = guid + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                if (isEditMode)
                {
                    string[] oldImagePart = imagePath.Split("/");
                    string oldImagePath = oldImagePart[^1];
                    string completeImageOldPath = Path.Combine(path, oldImagePath);

                    if (System.IO.File.Exists(completeImageOldPath))
                    {
                        System.IO.File.Delete(completeImageOldPath);
                    }
                }
                return $"{basePath}/{fileName}";
            }
        }
    }
}
