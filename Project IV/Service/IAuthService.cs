using Project_IV.Dtos;

namespace Project_IV.Service
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<string?> GetCurrentUserIdAsync();
    }
} 