using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<Response<VehicleUpdateResponse>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<VehiclePatchDto> PatchDocument { get; set; }
    }
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Response<VehicleUpdateResponse>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<Response<VehicleUpdateResponse>> Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(command.Id);
            if (vehicle == null)
            {
                throw new ApiException("Vehicle not found", (int)HttpStatusCode.NotFound);
            }

            if (command.PatchDocument == null)
            {
                throw new ApiException("Patch document is required", (int)HttpStatusCode.BadRequest);
            }

            var vehiclePatchDto = _mapper.Map<VehiclePatchDto>(vehicle);
            command.PatchDocument.ApplyTo(vehiclePatchDto);

            vehicle = _mapper.Map(vehiclePatchDto, vehicle);
            
            await _vehicleRepository.UpdateAsync(vehicle, vehicle.Id);

            var vehicleResponse = _mapper.Map<VehicleUpdateResponse>(vehicle);

            return new Response<VehicleUpdateResponse>(vehicleResponse);

        }
       
    }

}
