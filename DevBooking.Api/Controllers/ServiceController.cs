using System.Security.Claims;
using DevBooking.Application.DTOs.Service;
using DevBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevBooking.Application.Exceptions;

namespace DevBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceManagementService _serviceManagementService;

    public ServiceController(IServiceManagementService serviceManagementService)
    {
        _serviceManagementService = serviceManagementService;
    }

    [HttpPost]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> CreateService(CreateServiceRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            var result = await _serviceManagementService.CreateServiceAsync(userId, request);
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

    [HttpGet("by-developer/{developerProfileId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByDeveloper(int developerProfileId)
    {
        var services = await _serviceManagementService.GetByDeveloperProfileIdAsync(developerProfileId);
        return Ok(services);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllActive()
    {
        var services = await _serviceManagementService.GetAllActiveAsync();
        return Ok(services);
    }
}