namespace DevBooking.Application.DTOs.Auth;

public class AuthResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Token { get; set; }

    public DateTime? Expiration { get; set; }

    public UserDto? User { get; set; }
}