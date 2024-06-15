using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Features.Vehicles.Commands.CreateVehicle;
using XodoApp.Core.Application.Features.Vehicles.Commands.DeleteVehicleById;
using XodoApp.Core.Application.Features.Vehicles.Commands.UpdateVehicle;
using XodoApp.Core.Application.Features.Vehicles.Queries.GetAllVehicle;
using XodoApp.Core.Application.Features.Vehicles.Queries.GetVehicleById;
using XodoApp.Core.Application.ViewModels.Vehicles;

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
           Summary = "Vehículo por Id",
           Description = "Obtiene un vehículo filtrando por el Id del mismo")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetVehicleByIdQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Creación de vehículos",
            Description = "Recibe los parametros necesarios para crear un nuevo vehículo")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post(CreateVehiclesCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();


        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveVehicleViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Actualización de vehículos",
            Description = "Recibe un vehículo ya existente para guardar los cambios")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Put(int id, UpdateVehicleCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Elminar un vehículo",
            Description = "Recibe los parametros necesarios para eliminar un vehículo existente")]
        public async Task<IActionResult> Delete(int id)
        {

            await Mediator.Send(new DeleteVehicleByIdCommand { Id = id });
            return NoContent();

        }
    }
}
