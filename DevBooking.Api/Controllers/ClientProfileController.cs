using DevBooking.Application.DTOs.Client;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProfileController : ControllerBase
    {
        private readonly IClientProfileService _clientProfileService;

        public ClientProfileController(IClientProfileService clientProfileService)
        {
            _clientProfileService = clientProfileService;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateProfile(CreateClientProfileRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userId == null) return Unauthorized();

            try
            {
                var result = await _clientProfileService.CreateProfileAsync(userId.Value, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("me")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var profile = await _clientProfileService.GetByUserIdAsync(userId.Value);
            if (profile == null) return NotFound("you havent created a Client profile yet.");
            return Ok(profile);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _clientProfileService.GetAllAsync();
            return Ok(profiles);
        }
    }
}
