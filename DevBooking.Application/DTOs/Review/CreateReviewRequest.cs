namespace DevBooking.Application.DTOs.Review
{
    public class CreateReviewRequest
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}