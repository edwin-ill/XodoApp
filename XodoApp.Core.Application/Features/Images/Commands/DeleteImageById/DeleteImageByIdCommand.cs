using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Images.Commands.DeleteImageById
{
    public class DeleteImageByIdCommand : IRequest<Response<int>>
    {

        [SwaggerParameter(Description = "El id de la imagen que desea eliminar")]
        public int Id { get; set; }

        public class DeleteImageByIdCommandHandler : IRequestHandler<DeleteImageByIdCommand, Response<int>>
        {
            private readonly IImageRepository _imageRepository;

            public DeleteImageByIdCommandHandler(IImageRepository imageRepository)
            {
                _imageRepository = imageRepository;
            }
            public async Task<Response<int>> Handle(DeleteImageByIdCommand command, CancellationToken cancellationToken)
            {
                Environment.SetEnvironmentVariable("CLOUDINARY_URL", "cloudinary://347341592221484:btHkdLBCrNe6RXXmwcPJJFO9rQs@diyhxd1my");
                var image = await _imageRepository.GetByIdAsync(command.Id);
                if (image == null) throw new ApiException("Image not found", (int)HttpStatusCode.NotFound);
                if (!string.IsNullOrEmpty(image.PublicId))
                {
                    var _cloudinary = new Cloudinary();
                    var deletionParams = new DeletionParams(image.PublicId);
                    await _cloudinary.DestroyAsync(deletionParams);
                }
                await _imageRepository.DeleteAsync(image);

                return new Response<int>(image.Id);
            }
        }
    }
}
