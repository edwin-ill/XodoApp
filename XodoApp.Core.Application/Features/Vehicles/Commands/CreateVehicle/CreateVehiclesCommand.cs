using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle
{   
    public class CreateVehiclesCommand :IRequest<Response<int>>
    {
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

        public class CreateVehiclesCommandHandler : IRequestHandler<CreateVehiclesCommand, Response<int>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMapper _mapper;

            public CreateVehiclesCommandHandler(IVehicleRepository vehicleRepository, IMapper mapper)
            {
                _vehicleRepository = vehicleRepository;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateVehiclesCommand command, CancellationToken cancellationToken)
            {
                var vehicle = _mapper.Map<Vehicle>(command);
                await _vehicleRepository.AddAsync(vehicle);
                return new Response<int>(vehicle.Id);
            }
        }
    }
}
