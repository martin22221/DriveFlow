using DriveFlow.Core.DTOs.Reviews;

namespace DriveFlow.Core.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetByCarIdAsync(int carId);
    Task<int> CreateAsync(CreateReviewDto model, string userId);
    Task<bool> DeleteAsync(int id, string userId, bool isAdmin);
}
