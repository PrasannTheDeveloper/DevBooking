using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Application.DTOs.Client
{
    public class ClientProfileDto
    {
        public int Id { get; set; }

        // Links to ApplicationUser.Id
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public string ProfileImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ImageProfile { get; set; } 
    }
}
