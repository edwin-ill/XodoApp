using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Dealerships.Commands.DeleteDealershipById
{
    public class DeleteDealershipByIdCommand : IRequest<Response<int>>
    {    
       
        [SwaggerParameter(Description = "El id del dealer que desea eliminar")]
        public int Id { get; set; }
        
        public class DeleteDealershipByIdCommandHandler : IRequestHandler<DeleteDealershipByIdCommand, Response<int>>
        {
            private readonly IDealershipRepository _dealershipRepository;

            public DeleteDealershipByIdCommandHandler(IDealershipRepository dealershipRepository)
            {
                _dealershipRepository = dealershipRepository;
            }
            public async Task<Response<int>> Handle(DeleteDealershipByIdCommand command, CancellationToken cancellationToken)
            {
                var dealer = await _dealershipRepository.GetByIdAsync(command.Id);
                if (dealer == null) throw new ApiException("Dealership not found", (int)HttpStatusCode.NotFound);
                await _dealershipRepository.DeleteAsync(dealer);

                return new Response<int>(dealer.Id);
            }
        }
    }
}
