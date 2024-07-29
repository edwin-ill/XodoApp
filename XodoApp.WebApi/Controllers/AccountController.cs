
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Dtos.UserDtos;
using XodoApp.Core.Application.Features.Accounts.Commands.DeleteAccountById;
using XodoApp.Core.Application.Features.Accounts.Queries;
using XodoApp.Core.Application.Features.Vehicles.Commands.DeleteVehicleById;
using XodoApp.Core.Application.Interfaces.Services;

namespace XodoApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de membresia")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista de los usuarios",
            Description = "Devuelve a todos los usuarios del sistema")]
        public async Task<IActionResult> Get()
        {
            var users = await Mediator.Send(new GetAllAccountsQuery());

            if (users != null)
            {
                return Ok(users);
            }
            else if (users == null)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Login del usuario",
            Description = "Autentica al usuario en el sistema y retorna un JWT")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            var response = await _accountService.AuthenticateAsync(request);

            if (!response.HasError)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(new { message = response.Error });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("registeradmin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Registro del administrador",
            Description = "Recibe los parametros adecuados para registrar un nuevo usuario de tipo administrador")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAdminUserAsync(request, origin));
        }

        [HttpPost("forgot-password")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Olvide mi contraseña ",
            Description = "Permite al usuario iniciar el proceso para obtener una nueva contraseña")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("reset-password")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Reinicio de contraseña",
            Description = "Permite al usuario cambiar su contraseña actual por una nueva")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
           Summary = "Eliminar una cuenta",
           Description = "Recibe los parametros necesarios para eliminar ua cuenta existente")]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteAccountByIdCommand { Id = id });
            return NoContent();
        }
    }
}
