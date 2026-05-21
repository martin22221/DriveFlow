using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveFlow.Infrastructure.Data.Models;

public class Car
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerDay { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public string OwnerId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
