using DevBooking.Application.DTOs.Booking;

namespace DevBooking.Application.Interfaces;

public interface IBookingService
{
    Task<BookingDto> CreateBookingAsync(string clientId, CreateBookingRequest request);
    Task<List<BookingDto>> GetMyBookingsAsync(string clientId);
    Task<List<BookingDto>> GetBookingsForDeveloperAsync(int developerProfileId);
}