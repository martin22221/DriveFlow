using DriveFlow.Core.DTOs.Categories;
using DriveFlow.Core.Interfaces;
using DriveFlow.Infrastructure.Data;
using DriveFlow.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveFlow.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly DriveFlowDbContext db;

    public CategoryService(DriveFlowDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        return await db.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<int> CreateAsync(CreateCategoryDto model)
    {
        var category = new Category { Name = model.Name };

        await db.Categories.AddAsync(category);
        await db.SaveChangesAsync();

        return category.Id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await db.Categories.FindAsync(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);
        await db.SaveChangesAsync();

        return true;
    }
}
