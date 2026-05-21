using DriveFlow.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
                    ImageUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e",
                    Description = "Luxury performance sedan suitable for premium trips.",
                    CategoryId = 3,
                    OwnerId = admin.Id
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    PricePerDay = 55,
                    ImageUrl = "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb",
                    Description = "Reliable economy car with low fuel consumption.",
                    CategoryId = 1,
                    OwnerId = admin.Id
                },
                new Car
                {
                    Brand = "Audi",
                    Model = "Q7",
                    Year = 2022,
                    PricePerDay = 180,
                    ImageUrl = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6",
                    Description = "Spacious SUV suitable for family and business travel.",
                    CategoryId = 2,
                    OwnerId = admin.Id
                }
            );

            await db.SaveChangesAsync();
        }
    }
}
