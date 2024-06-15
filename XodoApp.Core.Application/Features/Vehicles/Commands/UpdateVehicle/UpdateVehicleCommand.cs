using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<Response<VehicleUpdateResponse>>
    {
        [SwaggerParameter(Description = "El Id del vehículo")]
        public int Id { get; set; }
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
            if (vehicle == null) throw new ApiException("Vehicle not found", (int)HttpStatusCode.NotFound);

            vehicle = _mapper.Map<Vehicle>(command);
            await _vehicleRepository.UpdateAsync(vehicle, vehicle.Id);

            var vehicleResponse = _mapper.Map<VehicleUpdateResponse>(vehicle);
            return new Response<VehicleUpdateResponse>(vehicleResponse);
        }
    }
}
