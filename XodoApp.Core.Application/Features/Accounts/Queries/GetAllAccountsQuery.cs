using AutoMapper;
using MediatR;
using System.Net;
using XodoApp.Core.Application.Dtos.UserDtos;
using XodoApp.Core.Application.Exceptions;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.Wrappers;

namespace XodoApp.Core.Application.Features.Accounts.Queries
{
    public class GetAllAccountsQuery : IRequest<Response<IList<UserDto>>>
    {
        public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, Response<IList<UserDto>>>
        {
            private readonly IAccountService _accountService;
            private readonly IMapper _mapper;

            public GetAllAccountsQueryHandler(IAccountService accountService, IMapper mapper)
            {
                _accountService = accountService;
                _mapper = mapper;
            }

            public async Task<Response<IList<UserDto>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
            {
                var accountList = await GetAllViewModel();
                if (accountList == null || accountList.Count == 0) throw new ApiException("Accounts not found", (int)HttpStatusCode.NotFound);
                return new Response<IList<UserDto>>(accountList);
            }

            private async Task<List<UserDto>> GetAllViewModel()
            {
                var accountList = await _accountService.GetUserDtosAsync();


                var listViewModels = _mapper.Map<List<UserDto>>(accountList);

                return listViewModels;
            }
        }
    }
}
