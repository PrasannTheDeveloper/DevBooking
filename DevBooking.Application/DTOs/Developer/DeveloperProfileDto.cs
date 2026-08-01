using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Application.DTOs.Developer;

public class DeveloperProfileDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;   // pulled from Identity
    public string Email { get; set; } = string.Empty;      // pulled from Identity
    public string Headline { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public string TechStack { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string? ProfileImageUrl { get; set; }
}