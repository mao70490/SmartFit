using SmartFit.Model;
using System.Security.Claims;

namespace SmartFit.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserRegister user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
