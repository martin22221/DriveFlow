using DriveFlow.Core.DTOs.Cars;
using DriveFlow.Core.Interfaces;
using DriveFlow.Infrastructure.Data;
using DriveFlow.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveFlow.Core.Services;

public class CarService : ICarService
{
    private readonly DriveFlowDbContext db;

    public CarService(DriveFlowDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<CarDto>> GetAllAsync(CarQueryDto query)
    {
        var carsQuery = db.Cars
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .Include(c => c.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            carsQuery = carsQuery.Where(c =>
                c.Brand.Contains(query.SearchTerm) ||
                c.Model.Contains(query.SearchTerm));
        }

        if (query.CategoryId.HasValue)
        {
            carsQuery = carsQuery.Where(c => c.CategoryId == query.CategoryId);
        }

        if (query.MinPrice.HasValue)
        {
            carsQuery = carsQuery.Where(c => c.PricePerDay >= query.MinPrice);
        }

        if (query.MaxPrice.HasValue)
        {
            carsQuery = carsQuery.Where(c => c.PricePerDay <= query.MaxPrice);
        }

        carsQuery = query.SortBy?.ToLower() switch
        {
            "price_asc" => carsQuery.OrderBy(c => c.PricePerDay),
            "price_desc" => carsQuery.OrderByDescending(c => c.PricePerDay),
            "year_desc" => carsQuery.OrderByDescending(c => c.Year),
            _ => carsQuery.OrderBy(c => c.Brand)
        };

        return await carsQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new CarDto
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                PricePerDay = c.PricePerDay,
                ImageUrl = c.ImageUrl,
                IsAvailable = c.IsAvailable,
                CategoryName = c.Category.Name
            })
            .ToListAsync();
    }

    public async Task<CarDto?> GetByIdAsync(int id)
    {
        return await db.Cars
            .AsNoTracking()
            .Include(c => c.Category)
            .Where(c => c.Id == id && !c.IsDeleted)
            .Select(c => new CarDto
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                PricePerDay = c.PricePerDay,
                ImageUrl = c.ImageUrl,
                IsAvailable = c.IsAvailable,
                CategoryName = c.Category.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(CreateCarDto model, string ownerId)
    {
        var car = new Car
        {
            Brand = model.Brand,
            Model = model.Model,
            Year = model.Year,
            PricePerDay = model.PricePerDay,
            ImageUrl = model.ImageUrl,
            Description = model.Description,
            CategoryId = model.CategoryId,
            OwnerId = ownerId,
            IsAvailable = true
        };

        await db.Cars.AddAsync(car);
        await db.SaveChangesAsync();

        return car.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCarDto model, string userId, bool isAdmin)
    {
        var car = await db.Cars.FindAsync(id);

        if (car == null || car.IsDeleted)
        {
            return false;
        }

        if (car.OwnerId != userId && !isAdmin)
        {
            return false;
        }

        car.Brand = model.Brand;
        car.Model = model.Model;
        car.Year = model.Year;
        car.PricePerDay = model.PricePerDay;
        car.ImageUrl = model.ImageUrl;
        car.Description = model.Description;
        car.CategoryId = model.CategoryId;
        car.IsAvailable = model.IsAvailable;

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, string userId, bool isAdmin)
    {
        var car = await db.Cars.FindAsync(id);

        if (car == null || car.IsDeleted)
        {
            return false;
        }

        if (car.OwnerId != userId && !isAdmin)
        {
            return false;
        }

        car.IsDeleted = true;
        await db.SaveChangesAsync();

        return true;
    }
}
