using Microsoft.AspNetCore.Identity.Data;
using SmartFit.Model;

namespace SmartFit.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
    }
}
