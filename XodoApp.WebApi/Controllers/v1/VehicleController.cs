using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle;
using XodoApp.Core.Application.Features.Vehicles.Commands.DeleteVehicleById;
using XodoApp.Core.Application.Features.Vehicles.Commands.UpdateVehicle;
using XodoApp.Core.Application.Features.Vehicles.Queries.GetAllVehicle;
using XodoApp.Core.Application.Features.Vehicles.Queries.GetVehicleById;
using XodoApp.Core.Application.Features.Vehicles.Queries.GetVehicleByVin;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XodoApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de vehículos")]
    public class VehicleController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de vehículos",
            Description = "Obtiene un listado de todos los vehículos")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GetAllVehiclesQuery()));

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehicleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Vehículo por VIN",
           Description = "Obtiene un vehículo filtrando por el VIN del mismo")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetVehicleByIdQuery { Id = id }));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de vehículos",
            Description = "Recibe los parametros necesarios para crear un nuevo vehículo")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post([FromBody] CreateVehiclesCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(command);
            var uri = Url.Action("GetVehicleByIdCommand", new { id = result.Data.Id });
            return Created(uri, result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehiclePatchDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Actualización parcial del vehículo ",
        Description = "Recibe un documento JSON PATCH para actualizar parcialmente el vehículo")]
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<VehiclePatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var command = new UpdateVehicleCommand
            {
                Id = id,
                PatchDocument = patchDocument
            };

            var result = await Mediator.Send(command);

            return result.Data != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Eliminar un vehículo",
            Description = "Recibe los parametros necesarios para eliminar un vehículo existente")]
        public async Task<IActionResult> Delete(int id)
        {

            await Mediator.Send(new DeleteVehicleByIdCommand { Id = id });
            return NoContent();

        }
    }
}
