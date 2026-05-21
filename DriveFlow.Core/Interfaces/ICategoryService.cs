using DriveFlow.Core.DTOs.Categories;

namespace DriveFlow.Core.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<int> CreateAsync(CreateCategoryDto model);
    Task<bool> DeleteAsync(int id);
}
