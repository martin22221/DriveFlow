using Microsoft.AspNetCore.Identity;

namespace DriveFlow.Infrastructure.Data.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string DrivingLicenseNumber { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public ICollection<Car> Cars { get; set; } = new List<Car>();

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
