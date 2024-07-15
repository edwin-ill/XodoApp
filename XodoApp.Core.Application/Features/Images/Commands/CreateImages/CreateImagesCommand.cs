using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Features.Images.Commands.CreateImage;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Images.Commands.CreateImage
{
    public class CreateImagesCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El url de la imagen")]
        public string ImageUrl { get; set; }
        [SwaggerParameter(Description = "El Id del vehiculo al cual esta imagen le corresponde")]
        public int VehicleId { get; set; }
        [SwaggerParameter(Description = "El id de la imagen en Cloudify")]
        public string PublicId { get; set; }


        public class CreateImagesCommandHandler : IRequestHandler<CreateImagesCommand, Response<int>>
        {
            private readonly IImageRepository _imageRepository;
            private readonly IMapper _mapper;

            public CreateImagesCommandHandler(IImageRepository imageRepository, IMapper mapper)
            {
                _imageRepository = imageRepository;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateImagesCommand command, CancellationToken cancellationToken)
            {
                var image = _mapper.Map<VehicleImage>(command);
                await _imageRepository.AddAsync(image);
                return new Response<int>(image.Id);
            }
        }
    }
}
