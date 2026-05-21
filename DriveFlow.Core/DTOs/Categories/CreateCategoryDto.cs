using System.ComponentModel.DataAnnotations;

namespace DriveFlow.Core.DTOs.Categories;

public class CreateCategoryDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
}
