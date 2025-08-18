using Microsoft.AspNetCore.Identity.Data;
using SmartFit.Model;

namespace SmartFit.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegister request);
        Task<UserRegister?> GetByEmailAsync(string email);
        Task<UserRegister?> GetByIdAsync(Guid id);
    }
}
