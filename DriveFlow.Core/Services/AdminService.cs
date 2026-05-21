using DriveFlow.Core.DTOs.Admin;
using DriveFlow.Core.Interfaces;
using DriveFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveFlow.Core.Services;

public class AdminService : IAdminService
{
    private readonly DriveFlowDbContext db;

    public AdminService(DriveFlowDbContext db)
    {
        this.db = db;
    }

    public async Task<AdminStatisticsDto> GetStatisticsAsync()
    {
        return new AdminStatisticsDto
        {
            CarsCount = await db.Cars.CountAsync(),
            UsersCount = await db.Users.CountAsync(),
            BookingsCount = await db.Bookings.CountAsync(),
            ReviewsCount = await db.Reviews.CountAsync()
        };
    }

    public async Task<IEnumerable<UserAdminDto>> GetUsersAsync()
    {
        return await db.Users
            .AsNoTracking()
            .Select(u => new UserAdminDto
            {
                Id = u.Id,
                Email = u.Email!,
                FullName = u.FirstName + " " + u.LastName
            })
            .ToListAsync();
    }
}
