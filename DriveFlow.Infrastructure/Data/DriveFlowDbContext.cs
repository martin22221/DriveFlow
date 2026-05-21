using DriveFlow.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveFlow.Infrastructure.Data;

public class DriveFlowDbContext : IdentityDbContext<ApplicationUser>
{
    public DriveFlowDbContext(DbContextOptions<DriveFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Booking> Bookings { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Car>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Cars)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Review>()
            .HasOne(r => r.Author)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Economy" },
            new Category { Id = 2, Name = "SUV" },
            new Category { Id = 3, Name = "Luxury" },
            new Category { Id = 4, Name = "Sport" }
        );
    }
}
