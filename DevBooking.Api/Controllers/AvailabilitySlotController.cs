using System.Security.Claims;
using DevBooking.Application.DTOs.Availability;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvailabilitySlotController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilitySlotController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpPost]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> CreateSlot(CreateAvailabilitySlotRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            var result = await _availabilityService.CreateSlotAsync(userId, request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("by-developer/{developerProfileId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByDeveloper(int developerProfileId)
    {
        var slots = await _availabilityService.GetByDeveloperProfileIdAsync(developerProfileId);
        return Ok(slots);
    }
}