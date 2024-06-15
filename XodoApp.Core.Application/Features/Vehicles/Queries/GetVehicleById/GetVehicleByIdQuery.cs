using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQuery : IRequest<Response<VehicleDto>>
    {
        [SwaggerParameter(Description = "El id del vehículo que desea buscar")]
        public int Id { get; set; }
        public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Response<VehicleDto>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMapper _mapper;

            public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
            {
                _vehicleRepository = vehicleRepository;
                _mapper = mapper;
            }

            public async Task<Response<VehicleDto>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
            {
                var vehicle = await GetByIdViewModel(request.Id);
                if (vehicle == null) throw new ApiException("Vehicle not found", (int)HttpStatusCode.NotFound);
                return new Response<VehicleDto>(vehicle);
            }

            private async Task<VehicleDto> GetByIdViewModel(int Id)
            {
                var vehicleList = await _vehicleRepository.GetAllAsync();

                var select = vehicleList.FirstOrDefault(f => f.Id == Id);
                var vehicleDto = _mapper.Map<VehicleDto>(select);
                return vehicleDto;
            }
        }
    }
}
