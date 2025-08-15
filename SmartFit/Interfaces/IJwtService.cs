using SmartFit.Model;
using System.Security.Claims;

namespace SmartFit.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
