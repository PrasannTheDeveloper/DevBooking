
using DevBooking.Application.DTOs.Booking;
using DevBooking.Application.Exceptions;
using DevBooking.Application.Interfaces;
using DevBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevBooking.Infrastructure.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IAvailabilitySlotRepository _slotRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IDeveloperProfileRepository _profileRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IAvailabilitySlotRepository slotRepository,
        IServiceRepository serviceRepository,
        IDeveloperProfileRepository profileRepository)
    {
        _bookingRepository = bookingRepository;
        _slotRepository = slotRepository;
        _serviceRepository = serviceRepository;
        _profileRepository = profileRepository;
    }

    public async Task<BookingDto> CreateBookingAsync(string clientId, CreateBookingRequest request)
    {
        // Rule: exactly one of ServiceId / AvailabilitySlotId must be set
        var hasService = request.ServiceId.HasValue;
        var hasSlot = request.AvailabilitySlotId.HasValue;

        if (hasService == hasSlot) // both true or both false — invalid either way
        {
            throw new BusinessRuleException("Provide exactly one of ServiceId or AvailabilitySlotId, not both or neither.");
        }

        var profile = await _profileRepository.GetByIdAsync(request.DeveloperProfileId);
        if (profile == null)
        {
            throw new NotFoundException("Developer profile not found.");
        }

        var booking = new Booking
        {
            ClientId = clientId,
            DeveloperProfileId = request.DeveloperProfileId,
            Notes = request.Notes,
            Status = BookingStatus.Pending
        };

        if (hasService)
        {
            var svc = await _serviceRepository.GetByIdAsync(request.ServiceId!.Value);

            if (svc == null || svc.DeveloperProfileId != request.DeveloperProfileId || !svc.IsActive)
            {
                throw new BusinessRuleException("Service not found or not offered by this developer.");
            }

            booking.ServiceId = svc.Id;

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();
        }
        else
        {
            var slot = await _slotRepository.GetByIdAsync(request.AvailabilitySlotId!.Value);

            if (slot == null || slot.DeveloperProfileId != request.DeveloperProfileId)
            {
                throw new NotFoundException("Availability slot not found for this developer.");
            }

            if (slot.IsBooked)
            {
                throw new ConflictException("This slot has already been booked.");
            }

            booking.AvailabilitySlotId = slot.Id;

            var success = await _bookingRepository.TryBookSlotAsync(booking, slot);

            if (!success)
            {
                throw new ConflictException("This slot was just booked by someone else. Please pick another.");
            }
        }

        return MapToDto(booking);
    }

    public async Task<List<BookingDto>> GetMyBookingsAsync(string clientId)
    {
        var bookings = await _bookingRepository.GetByClientIdAsync(clientId);
        return bookings.Select(MapToDto).ToList();
    }

    public async Task<List<BookingDto>> GetBookingsForDeveloperAsync(int developerProfileId)
    {
        var bookings = await _bookingRepository.GetByDeveloperProfileIdAsync(developerProfileId);
        return bookings.Select(MapToDto).ToList();
    }

    private static BookingDto MapToDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            ClientId = booking.ClientId,
            DeveloperProfileId = booking.DeveloperProfileId,
            ServiceId = booking.ServiceId,
            AvailabilitySlotId = booking.AvailabilitySlotId,
            Status = booking.Status.ToString(),
            Notes = booking.Notes,
            CreatedAt = booking.CreatedAt
        };
    }

    public async Task<BookingDto> UpdateBookingStatusAsync(string userId, int bookingId, string newStatus)
{
    var booking = await _bookingRepository.GetByIdAsync(bookingId);

    if (booking == null)
    {
        throw new NotFoundException("Booking not found.");
    }

    if (!Enum.TryParse<BookingStatus>(newStatus, true, out var parsedStatus))
    {
        throw new BusinessRuleException("Invalid status value.");
    }

    var isClient = booking.ClientId == userId;

    var profile = await _profileRepository.GetByUserIdAsync(userId);
    var isOwningFreelancer = profile != null && profile.Id == booking.DeveloperProfileId;

    if (!isClient && !isOwningFreelancer)
    {
        throw new UnauthorizedException("You are not authorized to update this booking.");
    }

    if (isClient)
    {
        // Clients can only cancel, and only while still Pending
        if (parsedStatus != BookingStatus.Cancelled)
        {
            throw new UnauthorizedException("Clients can only cancel a booking.");
        }

        if (booking.Status != BookingStatus.Pending)
        {
            throw new BusinessRuleException($"Cannot cancel a booking that is already {booking.Status}.");
        }
    }
    else
    {
        // Freelancer — full transition rules
        var allowedTransitions = new Dictionary<BookingStatus, BookingStatus[]>
        {
            [BookingStatus.Pending] = new[] { BookingStatus.Confirmed, BookingStatus.Cancelled },
            [BookingStatus.Confirmed] = new[] { BookingStatus.Completed, BookingStatus.Cancelled },
            [BookingStatus.Completed] = Array.Empty<BookingStatus>(),
            [BookingStatus.Cancelled] = Array.Empty<BookingStatus>()
        };

        if (!allowedTransitions[booking.Status].Contains(parsedStatus))
        {
            throw new BusinessRuleException($"Cannot change status from {booking.Status} to {parsedStatus}.");
        }
    }

    booking.Status = parsedStatus;
    await _bookingRepository.SaveChangesAsync();

    return MapToDto(booking);
}
}