using DevBooking.Application.DTOs.Client;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using DevBooking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DevBooking.Infrastructure.Services
{
    public class ClientProfileService : IClientProfileService
    {
        private readonly IClientProfileRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientProfileService(
            IClientProfileRepository repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<ClientProfileDto> CreateProfileAsync(string userId, CreateClientProfileRequest request)
        {
            var existing = await _repository.GetByUserIdAsync(userId);
            if (existing != null)
            {
                throw new InvalidOperationException("Client profile already exists for this user.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var profile = new ClientProfile
            {
                UserId = userId,
                CompanyName = request.CompanyName,
                JobTitle = request.JobTitle,
                Bio = request.Bio,
                Website = request.Website,
                Location = request.Location,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(profile);
            await _repository.SaveChangesAsync();

            return await MapToDto(profile);
        }

        public async Task<List<ClientProfileDto>> GetAllAsync()
        {
            var profiles = await _repository.GetAllAsync();
            var profileDtos = new List<ClientProfileDto>();

            foreach (var profile in profiles)
            {
                profileDtos.Add(await MapToDto(profile));
            }

            return profileDtos;
        }

        public async Task<ClientProfileDto?> GetByUserIdAsync(string userId)
        {
            var profile = await _repository.GetByUserIdAsync(userId);
            if (profile == null)
            {
                return null;
            }

            return await MapToDto(profile);
        }

        private async Task<ClientProfileDto> MapToDto(ClientProfile client)
        {
            var user = await _userManager.FindByIdAsync(client.UserId);

            return new ClientProfileDto
            {
                UserId = client.UserId,
                FullName = user?.FullName ?? string.Empty,
                Email = user?.Email ?? string.Empty,
                ProfileImageUrl = user?.ProfileImageUrl,
                CompanyName = client.CompanyName,
                JobTitle = client.JobTitle,
                Bio = client.Bio,
                Website = client.Website,
                Location = client.Location,
                CreatedAt = client.CreatedAt
            };
        }
    }
}