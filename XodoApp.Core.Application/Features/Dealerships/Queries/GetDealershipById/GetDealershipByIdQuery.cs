using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Dtos.Dealership;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Features.Dealerships.Queries.GetDealershipById;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Dealerships.Queries.GetDealershipById
{
    public class GetDealershipByIdQuery : IRequest<Response<DealershipDto>>
    {
        [SwaggerParameter(Description = "El id del dealer que desea buscar")]
        public int Id { get; set; }
        public class GetDealershipByIdQueryHandler : IRequestHandler<GetDealershipByIdQuery, Response<DealershipDto>>
        {
            private readonly IDealershipRepository _dealershipRepository;
            private readonly IMapper _mapper;

            public GetDealershipByIdQueryHandler(IDealershipRepository dealershipRepository, IMapper mapper)
            {
                _dealershipRepository = dealershipRepository;
                _mapper = mapper;
            }

            public async Task<Response<DealershipDto>> Handle(GetDealershipByIdQuery request, CancellationToken cancellationToken)
            {
                var dealership = await GetByIdViewModel(request.Id);
                if (dealership == null) throw new ApiException("Dealership not found", (int)HttpStatusCode.NotFound);
                return new Response<DealershipDto>(dealership);
            }

            private async Task<DealershipDto> GetByIdViewModel(int Id)
            {
                var dealershipList = await _dealershipRepository.GetAllAsync();

                var select = dealershipList.FirstOrDefault(f => f.Id == Id);
                var dealershipDto = _mapper.Map<DealershipDto>(select);
                return dealershipDto;
            }
        }
    }
}
