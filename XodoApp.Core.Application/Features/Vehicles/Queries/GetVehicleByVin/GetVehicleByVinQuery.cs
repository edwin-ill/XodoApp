using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Vehicles.Queries.GetVehicleByVin
{
    public class GetVehicleByVinQuery : IRequest<Response<VehicleDto>>
    {
        [SwaggerParameter(Description = "El Vin del vehículo que desea buscar")]
        public string Vin { get; set; }
        public class GetVehicleByVinQueryHandler : IRequestHandler<GetVehicleByVinQuery, Response<VehicleDto>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMapper _mapper;

            public GetVehicleByVinQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
            {
                _vehicleRepository = vehicleRepository;
                _mapper = mapper;
            }

            public async Task<Response<VehicleDto>> Handle(GetVehicleByVinQuery request, CancellationToken cancellationToken)
            {
                var vehicle = await GetByIdViewModel(request.Vin);
                if (vehicle == null) throw new ApiException("Vehicle not found", (int)HttpStatusCode.NotFound);
                return new Response<VehicleDto>(vehicle);
            }

            private async Task<VehicleDto> GetByIdViewModel(string Vin)
            {
                var vehicleList = await _vehicleRepository.GetAllWithImagesAsync();

                var select = vehicleList.FirstOrDefault(f => f.VIN == Vin);
                var vehicleDto = _mapper.Map<VehicleDto>(select);
                return vehicleDto;
            }
        }
    }
}
