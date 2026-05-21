using System.ComponentModel.DataAnnotations;

namespace DriveFlow.Infrastructure.Data.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
