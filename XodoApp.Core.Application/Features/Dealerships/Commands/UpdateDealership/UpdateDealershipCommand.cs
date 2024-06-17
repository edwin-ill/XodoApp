using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Features.Dealerships.Commands.UpdateDealership;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;
using XodoApp.Core.Domain.Entities;

namespace XodoApp.Core.Application.Features.Dealerships.Commands.UpdateDealership
{
    public class UpdateDealershipCommand : IRequest<Response<DealershipUpdateResponse>>
    {
        [SwaggerParameter(Description = "El nuevo id del dealer")]
        public int Id { get; set; }
        [SwaggerParameter(Description = "El nuevo nombre del dealer")]
        public string Name { get; set; }
        [SwaggerParameter(Description = "La nueva dirección del dealer")]
        public string Address { get; set; }
        [SwaggerParameter(Description = "La nueva ciudad del dealer")]
        public string City { get; set; }
        [SwaggerParameter(Description = "El nuevo telefono del dealer")]
        public string PhoneNumber { get; set; }
        [SwaggerParameter(Description = "El nuevo email del dealer")]
        public string Email { get; set; }
    }
    public class UpdateDealershipCommandHandler : IRequestHandler<UpdateDealershipCommand, Response<DealershipUpdateResponse>>
    {
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IMapper _mapper;

        public UpdateDealershipCommandHandler(IDealershipRepository dealershipRepository, IMapper mapper)
        {
            _dealershipRepository = dealershipRepository;
            _mapper = mapper;
        }
        public async Task<Response<DealershipUpdateResponse>> Handle(UpdateDealershipCommand command, CancellationToken cancellationToken)
        {
            var dealership = await _dealershipRepository.GetByIdAsync(command.Id);
            if (dealership == null) throw new ApiException("Dealership not found", (int)HttpStatusCode.NotFound);

            dealership = _mapper.Map<Dealership>(command);
            await _dealershipRepository.UpdateAsync(dealership, dealership.Id);

            var dealershipResponse = _mapper.Map<DealershipUpdateResponse>(dealership);
            return new Response<DealershipUpdateResponse>(dealershipResponse);
        }
    }
}
