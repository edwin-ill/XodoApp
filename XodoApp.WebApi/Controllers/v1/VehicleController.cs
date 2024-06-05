using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using XodoApp.Core.Application.Dtos.Vehicle;
using XodoApp.Core.Application.Features.Vehicles.Queries.GetAllVehicle;

namespace XodoApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de vehículos")]
    [Authorize(Roles = "Admin")]
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
    }
}
