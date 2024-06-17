using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using XodoApp.Core.Application.Dtos.Dealership;
using XodoApp.Core.Application.Features.Dealerships.Commands.CreateDealership;
using XodoApp.Core.Application.Features.Dealerships.Commands.DeleteDealershipById;
using XodoApp.Core.Application.Features.Dealerships.Commands.UpdateDealership;
using XodoApp.Core.Application.Features.Dealerships.Queries.GetAllDealership;
using XodoApp.Core.Application.Features.Dealerships.Queries.GetDealershipById;
using XodoApp.Core.Application.ViewModels.Dealerships;

namespace XodoApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de dealerships")]
    public class Dealership : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DealershipDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de dealers",
            Description = "Obtiene un listado de todos los dealers")]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GetAllDealershipsQuery()));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DealershipDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Dealership por Id",
           Description = "Obtiene un dealer filtrando por el Id del mismo")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDealershipByIdQuery { Id = id }));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de dealerships",
            Description = "Recibe los parametros necesarios para crear un nuevo dealer")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post([FromBody] CreateDealershipsCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();


        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveDealershipViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Actualización de dealers",
            Description = "Recibe un dealer ya existente para guardar los cambios")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDealershipCommand command)
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
            Summary = "Elminar un dealer",
            Description = "Recibe los parametros necesarios para eliminar un dealer existente")]
        public async Task<IActionResult> Delete(int id)
        {

            await Mediator.Send(new DeleteDealershipByIdCommand { Id = id });
            return NoContent();

        }
    }
}
