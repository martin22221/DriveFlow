namespace DriveFlow.Core.DTOs.Reviews;

public class ReviewDto
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string Content { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;
}
