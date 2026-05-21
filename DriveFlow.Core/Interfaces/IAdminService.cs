using DriveFlow.Core.DTOs.Admin;

namespace DriveFlow.Core.Interfaces;

public interface IAdminService
{
    Task<AdminStatisticsDto> GetStatisticsAsync();
    Task<IEnumerable<UserAdminDto>> GetUsersAsync();
}
