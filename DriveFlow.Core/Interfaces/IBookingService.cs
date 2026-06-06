using DriveFlow.Core.DTOs.Bookings;

namespace DriveFlow.Core.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetMyBookingsAsync(string userId);
    Task<int> CreateAsync(CreateBookingDto model, string userId);
    Task<bool> CancelAsync(int id, string userId);
}



                       