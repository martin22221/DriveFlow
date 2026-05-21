namespace DriveFlow.Core.DTOs.Cars;

public class CarDto
{
    public int Id { get; set; }

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public decimal PricePerDay { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public bool IsAvailable { get; set; }
}
