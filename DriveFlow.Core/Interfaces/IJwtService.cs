using DriveFlow.Infrastructure.Data.Models;

namespace DriveFlow.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
