using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Application.DTOs.Developer;

public class CreateDeveloperProfileRequest
{
    public string Headline { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public string TechStack { get; set; } = string.Empty;
}