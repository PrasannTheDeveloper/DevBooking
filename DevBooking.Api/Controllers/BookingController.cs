using System.Security.Claims;
using DevBooking.Application.DTOs.Booking;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (clientId == null)
        {
            return Unauthorized();
        }
        
        try
        {
            var result = await _bookingService.CreateBookingAsync(clientId, request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("mine")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetMyBookings()
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (clientId == null)
        {
            return Unauthorized();
        }

        var bookings = await _bookingService.GetMyBookingsAsync(clientId);
        return Ok(bookings);
    }

    [HttpGet("for-developer/{developerProfileId}")]
    [Authorize(Roles = "Freelancer,Admin")]
    public async Task<IActionResult> GetForDeveloper(int developerProfileId)
    {
        var bookings = await _bookingService.GetBookingsForDeveloperAsync(developerProfileId);
        return Ok(bookings);
    }

    [HttpPatch("{bookingId}/status")]
    [Authorize(Roles = "Freelancer,Client")]
    public async Task<IActionResult> UpdateStatus(int bookingId, [FromBody] string newStatus)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            var result = await _bookingService.UpdateBookingStatusAsync(userId, bookingId, newStatus);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}