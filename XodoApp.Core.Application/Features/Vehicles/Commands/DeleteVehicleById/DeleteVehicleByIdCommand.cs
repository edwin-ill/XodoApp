using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

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

        public DeleteVehicleByIdCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public async Task<Response<int>> Handle(DeleteVehicleByIdCommand command, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(command.Id);
            if (vehicle == null) throw new ApiException("Vehicle not found", (int)HttpStatusCode.NotFound);
            await _vehicleRepository.DeleteAsync(vehicle);

            return new Response<int>(vehicle.Id);
        }
    }
}
