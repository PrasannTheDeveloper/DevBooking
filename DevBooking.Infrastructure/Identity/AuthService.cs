using DevBooking.Application.DTOs.Auth;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DevBooking.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IFileStorageService _fileStorageService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        ,ITokenService tokenService
        ,IFileStorageService fileStorageService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _fileStorageService = fileStorageService;
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

        var token = _tokenService.GenerateToken(
            user.Id,
            user.Email!,
            user.FullName,
            roles);

        return new AuthResponse
        {
            Success = true,
            Message = "Login successful.",
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60), // matches JwtSettings.ExpiryMinutes
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

    public async Task<string> UpdateProfileImageAsync(string userId, Stream fileStream, string fileName, string contentType)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        // Delete old image if one exists, so old files don't pile up
        if (!string.IsNullOrEmpty(user.ProfileImageUrl))
        {
            await _fileStorageService.DeleteFileAsync(user.ProfileImageUrl);
        }

        var imageUrl = await _fileStorageService.UploadFileAsync(fileStream, fileName, contentType);

        user.ProfileImageUrl = imageUrl;
        await _userManager.UpdateAsync(user);

        return imageUrl;
    }
}