using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Dtos.UserDtos;
using XodoApp.Core.Application.ViewModels.Users;


namespace XodoApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task Delete(UserDto userDto);
    }
}
