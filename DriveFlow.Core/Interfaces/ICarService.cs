using DriveFlow.Core.DTOs.Cars;

namespace DriveFlow.Core.Interfaces;

public interface ICarService
{
    Task<IEnumerable<CarDto>> GetAllAsync(CarQueryDto query);
    Task<CarDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCarDto model, string ownerId);
    Task<bool> UpdateAsync(int id, UpdateCarDto model, string userId, bool isAdmin);
    Task<bool> DeleteAsync(int id, string userId, bool isAdmin);
}
