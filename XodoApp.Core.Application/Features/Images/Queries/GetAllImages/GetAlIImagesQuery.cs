using AutoMapper;
using MediatR;
using System.Net;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Images.Queries.GetAllImage
{
    public class GetAllImagesQuery : IRequest<Response<IList<VehicleImageDto>>>
    {
        public class GetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, Response<IList<VehicleImageDto>>>
        {
            private readonly IImageRepository _imageRepository;
            private readonly IMapper _mapper;

            public GetAllImagesQueryHandler(IImageRepository imageRepository, IMapper mapper)
            {
                _imageRepository = imageRepository;
                _mapper = mapper;
            }

            public async Task<Response<IList<VehicleImageDto>>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
            {
                var imageList = await GetAllViewModel();
                if (imageList == null || imageList.Count == 0) throw new ApiException("Images not found", (int)HttpStatusCode.NotFound);
                return new Response<IList<VehicleImageDto>>(imageList);
            }

            private async Task<List<VehicleImageDto>> GetAllViewModel()
            {
                var imageList = await _imageRepository.GetAllAsync();


                var listViewModels = _mapper.Map<List<VehicleImageDto>>(imageList);

                return listViewModels;
            }
        }
    }
}
