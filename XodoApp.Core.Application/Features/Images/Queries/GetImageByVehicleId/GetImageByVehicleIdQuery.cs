using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Images.Queries.GetImageByVehicleId
{
    public class GetImageByVehicleIdQuery : IRequest<Response<IList<VehicleImageDto>>>
    {
        [SwaggerParameter(Description = "El id del vehiculo que tiene las imagenes que desea buscar")]
        public int Id { get; set; }

        public class GetImageByVehicleIdQueryHandler : IRequestHandler<GetImageByVehicleIdQuery, Response<IList<VehicleImageDto>>>
        {
            private readonly IImageRepository _imageRepository;
            private readonly IMapper _mapper;

            public GetImageByVehicleIdQueryHandler(IImageRepository imageRepository, IMapper mapper)
            {
                _imageRepository = imageRepository;
                _mapper = mapper;
            }

            public async Task<Response<IList<VehicleImageDto>>> Handle(GetImageByVehicleIdQuery request, CancellationToken cancellationToken)
            {
                var image = await GetByVehicleIdViewModel(request.Id);
                if (image == null || !image.Any())
                {
                    throw new ApiException("Images not found", (int)HttpStatusCode.NotFound);
                }
                return new Response<IList<VehicleImageDto>>(image);
            }

            private async Task<List<VehicleImageDto>> GetByVehicleIdViewModel(int id)
            {
                var imageList = await _imageRepository.GetAllAsync();
                var select = imageList.Where(x => x.VehicleId == id);
                var imageDtoList = _mapper.Map<List<VehicleImageDto>>(select);
                return imageDtoList;
            }
        }
    }
}
