using DevBooking.Application.DTOs.Developer;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using DevBooking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DevBooking.Infrastructure.Services;

public class DeveloperProfileService : IDeveloperProfileService
{
    private readonly IDeveloperProfileRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeveloperProfileService(
        IDeveloperProfileRepository repository,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    public async Task<DeveloperProfileDto> CreateProfileAsync(string userId, CreateDeveloperProfileRequest request)
    {
        var existing = await _repository.GetByUserIdAsync(userId);
        if (existing != null)
        {
            throw new InvalidOperationException("Developer profile already exists for this user.");
        }

        var profile = new DeveloperProfile
        {
            UserId = userId,
            Headline = request.Headline,
            Bio = request.Bio,
            HourlyRate = request.HourlyRate,
            TechStack = request.TechStack
        };

        await _repository.AddAsync(profile);
        await _repository.SaveChangesAsync();

        return await MapToDto(profile);
    }

    public async Task<DeveloperProfileDto?> GetByUserIdAsync(string userId)
    {
        var profile = await _repository.GetByUserIdAsync(userId);
        return profile == null ? null : await MapToDto(profile);
    }

    public async Task<List<DeveloperProfileDto>> GetAllAsync()
    {
        var profiles = await _repository.GetAllAsync();
        var dtos = new List<DeveloperProfileDto>();

        foreach (var profile in profiles)
        {
            dtos.Add(await MapToDto(profile));
        }

        return dtos;
    }

    private async Task<DeveloperProfileDto> MapToDto(DeveloperProfile profile)
    {
        var user = await _userManager.FindByIdAsync(profile.UserId);

        return new DeveloperProfileDto
        {
            Id = profile.Id,
            UserId = profile.UserId,
            FullName = user?.FullName ?? string.Empty,
            Email = user?.Email ?? string.Empty,
            Headline = profile.Headline,
            Bio = profile.Bio,
            HourlyRate = profile.HourlyRate,
            TechStack = profile.TechStack,
            IsAvailable = profile.IsAvailable,
            ProfileImageUrl = user.ProfileImageUrl
        };
    }
}

