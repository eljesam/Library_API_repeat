using Library_API_repeat.Api.DTOs.Authentication;
namespace Library_API_repeat.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> EmailExistsAsync(string email);

        Task<bool> RegisterAsync(RegisterDTO dto);
        Task<AuthResponseDTO?> LoginAsync(LoginDTO dto);
    }
}
