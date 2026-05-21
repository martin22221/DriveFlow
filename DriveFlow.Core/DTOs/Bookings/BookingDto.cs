namespace DriveFlow.Core.DTOs.Bookings;

public class BookingDto
{
    public int Id { get; set; }

    public string CarName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalPrice { get; set; }
}
