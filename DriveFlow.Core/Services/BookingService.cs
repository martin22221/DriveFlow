using DriveFlow.Core.DTOs.Bookings;
using DriveFlow.Core.Interfaces;
using DriveFlow.Infrastructure.Data;
using DriveFlow.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveFlow.Core.Services;

public class BookingService : IBookingService
{
    private readonly DriveFlowDbContext db;

    public BookingService(DriveFlowDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<BookingDto>> GetMyBookingsAsync(string userId)
    {
        return await db.Bookings
            .AsNoTracking()
            .Include(b => b.Car)
            .Where(b => b.UserId == userId && !b.IsCancelled)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                CarName = b.Car.Brand + " " + b.Car.Model,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalPrice = b.TotalPrice
            })
            .ToListAsync();
    }

    public async Task<int> CreateAsync(CreateBookingDto model, string userId)
    {
        if (model.EndDate <= model.StartDate)
        {
            throw new InvalidOperationException("End date must be after start date.");
        }

        var car = await db.Cars.FirstOrDefaultAsync(c => c.Id == model.CarId && !c.IsDeleted);

        if (car == null || !car.IsAvailable)
        {
            throw new InvalidOperationException("Car is not available.");
        }

        var days = (model.EndDate.Date - model.StartDate.Date).Days;
        var totalPrice = days * car.PricePerDay;

        var booking = new Booking
        {
            CarId = model.CarId,
            UserId = userId,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            TotalPrice = totalPrice
        };

        await db.Bookings.AddAsync(booking);
        await db.SaveChangesAsync();

        return booking.Id;
    }

    public async Task<bool> CancelAsync(int id, string userId)
    {
        var booking = await db.Bookings.FindAsync(id);

        if (booking == null || booking.UserId != userId)
        {
            return false;
        }

        booking.IsCancelled = true;
        await db.SaveChangesAsync();

        return true;
    }
}
