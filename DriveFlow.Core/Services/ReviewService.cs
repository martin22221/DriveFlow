using DriveFlow.Core.DTOs.Reviews;
using DriveFlow.Core.Interfaces;
using DriveFlow.Infrastructure.Data;
using DriveFlow.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveFlow.Core.Services;

public class ReviewService : IReviewService
{
    private readonly DriveFlowDbContext db;

    public ReviewService(DriveFlowDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<ReviewDto>> GetByCarIdAsync(int carId)
    {
        return await db.Reviews
            .AsNoTracking()
            .Include(r => r.Author)
            .Where(r => r.CarId == carId)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Content = r.Content,
                AuthorName = r.Author.FirstName + " " + r.Author.LastName
            })
            .ToListAsync();
    }

    public async Task<int> CreateAsync(CreateReviewDto model, string userId)
    {
        var review = new Review
        {
            CarId = model.CarId,
            Rating = model.Rating,
            Content = model.Content,
            AuthorId = userId
        };

        await db.Reviews.AddAsync(review);
        await db.SaveChangesAsync();

        return review.Id;
    }

    public async Task<bool> DeleteAsync(int id, string userId, bool isAdmin)
    {
        var review = await db.Reviews.FindAsync(id);

        if (review == null)
        {
            return false;
        }

        if (review.AuthorId != userId && !isAdmin)
        {
            return false;
        }

        db.Reviews.Remove(review);
        await db.SaveChangesAsync();

        return true;
    }
}
