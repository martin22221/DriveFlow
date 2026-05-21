namespace DriveFlow.Core.DTOs.Cars;

public class UpdateCarDto : CreateCarDto
{
    public bool IsAvailable { get; set; } = true;
}
