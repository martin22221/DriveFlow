using DriveFlow.Core.DTOs.Cars;
using DriveFlow.Core.Services;
using DriveFlow.Infrastructure.Data;
using DriveFlow.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DriveFlow.Tests;

public class CarServiceTests
{
    private DriveFlowDbContext db = null!;
    private CarService service = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DriveFlowDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        db = new DriveFlowDbContext(options);

        db.Categories.Add(new Category { Id = 1, Name = "SUV" });
        db.Users.Add(new ApplicationUser
        {
            Id = "user-1",
            UserName = "test@test.com",
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "User",
            DrivingLicenseNumber = "DL123"
        });

        db.SaveChanges();

        service = new CarService(db);
    }

    [Test]
    public async Task CreateAsync_ShouldCreateCar()
    {
        var dto = new CreateCarDto
        {
            Brand = "BMW",
            Model = "X5",
            Year = 2022,
            PricePerDay = 150,
            ImageUrl = "https://example.com/car.jpg",
            Description = "Test description for car.",
            CategoryId = 1
        };

        var id = await service.CreateAsync(dto, "user-1");

        Assert.That(id, Is.GreaterThan(0));
        Assert.That(db.Cars.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnCars()
    {
        await service.CreateAsync(new CreateCarDto
        {
            Brand = "Audi",
            Model = "Q7",
            Year = 2020,
            PricePerDay = 120,
            ImageUrl = "https://example.com/audi.jpg",
            Description = "Nice SUV vehicle.",
            CategoryId = 1
        }, "user-1");

        var result = await service.GetAllAsync(new CarQueryDto());

        Assert.That(result.Count(), Is.EqualTo(1));
    }
}
