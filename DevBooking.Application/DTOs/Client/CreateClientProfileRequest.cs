using System;
using System.Collections.Generic;
using System.Text;

namespace DevBooking.Application.DTOs.Client
{
    public class CreateClientProfileRequest
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
