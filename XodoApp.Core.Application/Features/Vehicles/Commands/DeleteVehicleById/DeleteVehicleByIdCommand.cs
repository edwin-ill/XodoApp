using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Vehicles.Commands.DeleteVehicleById
{
    public class DeleteVehicleByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id del vehículo que desea eliminar")]
        public int Id { get; set; }
    }

    public class DeleteVehicleByIdCommandHandler : IRequestHandler<DeleteVehicleByIdCommand, Response<int>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IImageRepository _imageRepository;

        public DeleteVehicleByIdCommandHandler(IVehicleRepository vehicleRepository, IImageRepository imageRepository)
        {
            _vehicleRepository = vehicleRepository;
            _imageRepository = imageRepository;
        }
        public async Task<Response<int>> Handle(DeleteVehicleByIdCommand command, CancellationToken cancellationToken)
        {
            Environment.SetEnvironmentVariable("CLOUDINARY_URL", "cloudinary://347341592221484:btHkdLBCrNe6RXXmwcPJJFO9rQs@diyhxd1my");

            var vehicle = await _vehicleRepository.GetByIdWithImages(command.Id);
            if (vehicle == null) throw new ApiException("Vehicle not found", (int)HttpStatusCode.NotFound);

            var _cloudinary = new Cloudinary();

            var imagesToDeleteFromCloudinary = new List<VehicleImage>();
            var imagesToDeleteFromRepository = new List<VehicleImage>();

            foreach (var image in vehicle.VehicleImages)
            {
                if (!string.IsNullOrEmpty(image.PublicId))
                {
                    imagesToDeleteFromCloudinary.Add(image);
                }
                else
                {
                    imagesToDeleteFromRepository.Add(image);
                }
            }

            foreach (var image in imagesToDeleteFromCloudinary)
            {
                var deletionParams = new DeletionParams(image.PublicId);
                await _cloudinary.DestroyAsync(deletionParams);
            }

            foreach (var image in imagesToDeleteFromRepository)
            {
                await _imageRepository.DeleteAsync(image);
            }

            await _vehicleRepository.DeleteAsync(vehicle);

            return new Response<int>(vehicle.Id);
        }
    }
}
