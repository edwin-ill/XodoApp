using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Features.Images.Commands.CreateImage;
using XodoApp.Core.Application.Features.Images.Commands.DeleteImageById;
using XodoApp.Core.Application.Features.Images.Queries.GetAllImage;
using XodoApp.Core.Application.Features.Images.Queries.GetImageById;
using XodoApp.Core.Application.Features.Images.Queries.GetImageByVehicleId;

namespace XodoApp.WebApi.Controllers.v1
{
    public class ImageController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleImageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de imagenes",
            Description = "Obtiene un listado de todas las imagenes")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GetAllImagesQuery()));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleImageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Imagen por Id",
           Description = "Obtiene una imagen filtrando por el Id de la misma")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetImageByIdQuery { Id = id }));
        }

        [HttpGet("ByVehicle/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleImageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Imagenes por Id del vehiculo",
           Description = "Obtiene imagens filtrando por el Id del vehiculo al cual corresponde")]
        public async Task<IActionResult> GetByVehicleId(int id)
        {
            return Ok(await Mediator.Send(new GetImageByVehicleIdQuery { Id = id }));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de las imagenes",
            Description = "Recibe los parametros necesarios para crear una nueva imagen")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post([FromBody] CreateImagesCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Eliminar una imagen",
            Description = "Recibe los parametros necesarios para eliminar una imagen existente")]
        public async Task<IActionResult> Delete(int id)
        {

            await Mediator.Send(new DeleteImageByIdCommand { Id = id });
            return NoContent();

        }        
    }
}
