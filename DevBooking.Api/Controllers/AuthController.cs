using System.Security.Claims;
using DevBooking.Application.DTOs.Auth;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DevBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        if (!result.Success)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.LogoutAsync();

        return Ok(result);
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok("Your are an admin , Congrats.. !");
    }

    [HttpGet("client-or-freelancer")]
    [Authorize(Roles = "Client,Freelancer")]
    public IActionResult ClientOrFreelancer()
    {
        return Ok("You're either a Client or a Freelancer.");
    }

    [HttpPost("upload-profile-image")]
    [Authorize]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType))
        {
            return BadRequest("Only JPG, PNG, or WEBP images are allowed.");
        }

        const long maxSizeBytes = 5 * 1024 * 1024; // 5 MB
        if (file.Length > maxSizeBytes)
        {
            return BadRequest("File size must not exceed 5MB.");
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        using var stream = file.OpenReadStream();
        var imageUrl = await _authService.UpdateProfileImageAsync(userId, stream, file.FileName, file.ContentType);

        return Ok(new { imageUrl });
    }
}