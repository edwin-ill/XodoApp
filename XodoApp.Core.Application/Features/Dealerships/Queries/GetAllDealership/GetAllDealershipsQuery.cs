using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XodoApp.Core.Application.Dtos.Dealership;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Features.Dealerships.Queries.GetAllDealership;
using XodoApp.Core.Application.Interfaces.Repositories;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Dealerships.Queries.GetAllDealership
{
    public class GetAllDealershipsQuery : IRequest<Response<IList<DealershipDto>>>
    {
        public class GetAllDealershipsQueryHandler : IRequestHandler<GetAllDealershipsQuery, Response<IList<DealershipDto>>>
        {
            private readonly IDealershipRepository _dealershipRepository;
            private readonly IMapper _mapper;

            public GetAllDealershipsQueryHandler(IDealershipRepository dealershipRepository, IMapper mapper)
            {
                _dealershipRepository = dealershipRepository;
                _mapper = mapper;
            }

            public async Task<Response<IList<DealershipDto>>> Handle(GetAllDealershipsQuery request, CancellationToken cancellationToken)
            {
                var dealershipList = await GetAllViewModel();
                if (dealershipList == null || dealershipList.Count == 0) throw new ApiException("Dealerships not found", (int)HttpStatusCode.NotFound);
                return new Response<IList<DealershipDto>>(dealershipList);
            }

            private async Task<List<DealershipDto>> GetAllViewModel()
            {
                var dealershipList = await _dealershipRepository.GetAllAsync();


                var listViewModels = _mapper.Map<List<DealershipDto>>(dealershipList);

                return listViewModels;
            }
        }
    }
}

