using DriveFlow.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.WebRequestMethods;

namespace DriveFlow.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var db = scope.ServiceProvider.GetRequiredService<DriveFlowDbContext>();

        string[] roles = ["Administrator", "User"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@driveflow.com";
        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                DrivingLicenseNumber = "ADMIN-0001"
            };

            await userManager.CreateAsync(admin, "Admin123");
            await userManager.AddToRoleAsync(admin, "Administrator");
        }

        if (!db.Cars.Any())
        {
            db.Cars.AddRange(
                new Car
                {
                    Brand = "BMW",
                    Model = "M5",
                    Year = 2021,
                    PricePerDay = 200,
                    ImageUrl = "https://cimg3.ibsrv.net/ibimg/hgm/1920x1080-1/100/749/2021-bmw-5-series_100749427.jpg",
                    Description = "Luxury performance sedan suitable for premium trips.",
                    CategoryId = 3,
                    OwnerId = admin.Id,
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    PricePerDay = 55,
                    ImageUrl = "https://scene7.toyota.eu/is/image/toyotaeurope/COR0001a_25_WEB_CROP:Large-Landscape?ts=0&resMode=sharp2&op_usm=1.75,0.3,2,0&fmt=png-alpha",
                    Description = "Reliable economy car with low fuel consumption.",
                    CategoryId = 1,
                    OwnerId = admin.Id,
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Audi",
                    Model = "Q7",
                    Year = 2022,
                    PricePerDay = 180,
                    ImageUrl = "https://media.ed.edmunds-media.com/audi/q7/2026/oem/2026_audi_q7_4dr-suv_premium-plus_fq_oem_4_1600.jpg",
                    Description = "Spacious SUV suitable for family and business travel.",
                    CategoryId = 2,
                    OwnerId = admin.Id,
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Lamborghini",
                    Model = "Huracan",
                    Year = 2023,
                    PricePerDay = 650,
                    ImageUrl = "https://m.netinfo.bg/media/images/50787/50787082/1180-663-lamborghini-huracan-stj.jpg",
                    Description = "Italian supercar with V10 engine.",
                    CategoryId = 3,
                    OwnerId = admin.Id,
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Tesla",
                    Model = "Model S Plaid",
                    Year = 2024,
                    PricePerDay = 280,
                    ImageUrl = "https://images.unsplash.com/photo-1560958089-b8a1929cea89",
                    Description = "High-performance electric luxury sedan.",
                    CategoryId = 2,
                    OwnerId = admin.Id,
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Porsche",
                    Model = "911 Turbo S",
                    Year = 2022,
                    PricePerDay = 500,
                    ImageUrl = "https://images.unsplash.com/photo-1503376780353-7e6692767b70",
                    Description = "Premium sports car with exceptional performance.",
                    CategoryId = 3,
                    OwnerId = admin.Id,
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Range Rover",
                    Model = "Sport",
                    Year = 2023,
                    PricePerDay = 320,
                    ImageUrl = "https://media.cdn-jaguarlandrover.com/api/v2/images/118063/w/1600/h/900.jpg",
                    Description = "Luxury SUV suitable for long trips.",
                    CategoryId = 2,
                    OwnerId = admin.Id,
                    IsAvailable = true
                }
            );

            await db.SaveChangesAsync();
        }
    }
}