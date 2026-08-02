using DevBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Application.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string ClientId { get; set; } = string.Empty;
        public int DeveloperProfileId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
