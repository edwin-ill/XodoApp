using AutoMapper;
using MediatR;
using System.Net;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Vehicles.Queries.GetAllVehicle
{
    public class GetAllVehiclesQuery : IRequest<Response<IList<VehicleDto>>>
    {
        public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, Response<IList<VehicleDto>>>
        {
            private readonly IVehicleRepository _vehicleRepository;
            private readonly IMapper _mapper;

            public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
            {
                _vehicleRepository = vehicleRepository;
                _mapper = mapper;
            }

            public async Task<Response<IList<VehicleDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
            {
                var vehicleList = await GetAllViewModel();
                if (vehicleList == null || vehicleList.Count == 0) throw new ApiException("Vehicles not found", (int)HttpStatusCode.NotFound);
                return new Response<IList<VehicleDto>>(vehicleList);
            }

            private async Task<List<VehicleDto>> GetAllViewModel()
            {
                var vehicleList = await _vehicleRepository.GetAllAsync();


                var listViewModels = _mapper.Map<List<VehicleDto>>(vehicleList);

                return listViewModels;
            }
        }
    }
}
