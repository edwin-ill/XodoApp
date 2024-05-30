using XodoApp.Core.Application.Dtos.Account;
using XodoApp.Core.Application.Dtos.UserDtos;
using System.Threading.Tasks;

namespace XodoApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
        Task<List<UserDto>> GetUserDtosAsync();
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<UserDto> FindById(string Id);
        Task Activate(UserDto userDto);
        Task Deactivate(UserDto userDto);
        Task Update(UpdateUserDto user);
        Task SignOutAsync();
        Task Delete(string userId);
    }
}