using DevBooking.Application.DTOs.Auth;

namespace DevBooking.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> LogoutAsync();
    Task<string> UpdateProfileImageAsync(string userId, Stream fileStream, string fileName, string contentType);
}