using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Images.Queries.GetImageById
{
    public class GetImageByIdQuery : IRequest<Response<VehicleImageDto>>
    {
        [SwaggerParameter(Description = "El id de la imagen que desea buscar")]
        public int Id { get; set; }
        public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, Response<VehicleImageDto>>
        {
            private readonly IImageRepository _imageRepository;
            private readonly IMapper _mapper;

            public GetImageByIdQueryHandler(IImageRepository imageRepository, IMapper mapper)
            {
                _imageRepository = imageRepository;
                _mapper = mapper;
            }

            public async Task<Response<VehicleImageDto>> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
            {
                var image = await GetByIdViewModel(request.Id);
                if (image == null) throw new ApiException("Image not found", (int)HttpStatusCode.NotFound);
                return new Response<VehicleImageDto>(image);
            }

            private async Task<VehicleImageDto> GetByIdViewModel(int Id)
            {
                var imageList = await _imageRepository.GetAllAsync();

                var select = imageList.FirstOrDefault(f => f.Id == Id);
                var imageDto = _mapper.Map<VehicleImageDto>(select);
                return imageDto;
            }
        }
    }
}
