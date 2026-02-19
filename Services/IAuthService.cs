using LibraryManagementAPI.DTOs.Auth;
using LibraryManagementAPI.DTOs.Loans;

namespace LibraryManagementAPI.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task RegisterAsync(RegisterRequestDto request);
    }
}
