using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Accounts.Commands.DeleteAccountById
{
    public class DeleteAccountByIdCommand : IRequest<Response<int>>
    {

        [SwaggerParameter(Description = "El id de la cuenta que desea eliminar")]
        public string Id { get; set; }

        public class DeleteAccountByIdCommandHandler : IRequestHandler<DeleteAccountByIdCommand, Response<int>>
        {
            private readonly IAccountService _accountService;

            public DeleteAccountByIdCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }
            public async Task<Response<int>> Handle(DeleteAccountByIdCommand command, CancellationToken cancellationToken)
            {
                var account = await _accountService.FindById(command.Id);
                if (account == null) throw new ApiException("Account not found", (int)HttpStatusCode.NotFound);
                await _accountService.Delete(command.Id);

                return new Response<int>(account.Id);
            }
        }
    }
}
