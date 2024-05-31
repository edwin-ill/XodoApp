﻿using AutoMapper;
using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Dtos.UserDtos;
using XodoApp.Core.Application.Interfaces.Services;
using XodoApp.Core.Application.ViewModels.Users;

namespace XodoApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            if (vm.Role == "Admin")
            {
                return await _accountService.RegisterAdminUserAsync(registerRequest, origin);
            }           
            else
            {
                return await _accountService.RegisterBasicUserAsync(registerRequest, origin);

            }
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        
        public async Task Delete(UserDto userDto)
        {
            await _accountService.Delete(userDto.Id);          

        }
    }
}
