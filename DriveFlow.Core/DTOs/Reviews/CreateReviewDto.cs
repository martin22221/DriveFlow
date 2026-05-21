using System.ComponentModel.DataAnnotations;

namespace DriveFlow.Core.DTOs.Reviews;

public class CreateReviewDto
{
    [Range(1, int.MaxValue)]
    public int CarId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 3)]
    public string Content { get; set; } = string.Empty;
}
