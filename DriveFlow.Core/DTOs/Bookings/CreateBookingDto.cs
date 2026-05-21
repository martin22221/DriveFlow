using System.ComponentModel.DataAnnotations;

namespace DriveFlow.Core.DTOs.Bookings;

public class CreateBookingDto
{
    [Range(1, int.MaxValue)]
    public int CarId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}
