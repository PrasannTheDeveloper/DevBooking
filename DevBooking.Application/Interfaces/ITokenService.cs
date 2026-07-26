using System.Security.Claims;

namespace DevBooking.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(string userId, string email, string fullName, IList<string> roles);
}