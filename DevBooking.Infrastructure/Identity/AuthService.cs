using DevBooking.Application.DTOs.Auth;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DevBooking.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Passwords do not match."
            };
        }

        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Success = false,
                Message = string.Join(", ", result.Errors.Select(x => x.Description))
            };
        }

        await _userManager.AddToRoleAsync(user, request.Role);

        return new AuthResponse
        {
            Success = true,
            Message = "User registered successfully.",
            User = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = request.Role
            }
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            false);

        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        return new AuthResponse
        {
            Success = true,
            Message = "Login successful.",
            Token = "", // JWT will be added next
            Expiration = null,
            User = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? string.Empty
            }
        };
    }

    public async Task<AuthResponse> LogoutAsync()
    {
        await _signInManager.SignOutAsync();

        return new AuthResponse
        {
            Success = true,
            Message = "Logout successful."
        };
    }
}