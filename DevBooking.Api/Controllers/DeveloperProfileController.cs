using DevBooking.Application.DTOs.Developer;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevBooking.Application.Exceptions;
using System.Security.Claims;

namespace DevBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperProfileController : ControllerBase
    {
        private readonly IDeveloperProfileService _profileService;

        public DeveloperProfileController(IDeveloperProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public async Task<IActionResult> CreateProfile(CreateDeveloperProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var result = await _profileService.CreateProfileAsync(userId, request);
                return Ok(result);
            }
            catch (BaseException ex)
            {
                return ex switch
                {
                    NotFoundException => NotFound(ex.Message),
                    ConflictException => Conflict(ex.Message),
                    ForbiddenException => StatusCode(403, ex.Message),
                    UnauthorizedException => Unauthorized(ex.Message),
                    ValidationException => BadRequest(ex.Message),
                    BusinessRuleException => BadRequest(ex.Message),
                    _ => BadRequest(ex.Message)
                };
            }
        }

        [HttpGet("me")]
        [Authorize(Roles ="Freelancer")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userid == null)
            {
                return Unauthorized();
            }
            var profile = await _profileService.GetByUserIdAsync(userid);
            if(profile == null)
            {
                return NotFound("you havent created a developer profile yet.");
            }
            return Ok(profile);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var allprofiles = await _profileService.GetAllAsync();
            return Ok(allprofiles);
        }
    }
}
