using System.ComponentModel.DataAnnotations;

namespace DriveFlow.Core.DTOs.Cars;

public class CreateCarDto
{
    [Required]
    [StringLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string Model { get; set; } = string.Empty;

    [Range(1990, 2030)]
    public int Year { get; set; }

    [Range(1, 10000)]
    public decimal PricePerDay { get; set; }

    [Required]
    [Url]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
