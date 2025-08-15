using SmartFit.Model;

namespace SmartFit.Interfaces
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(UserLoginRequest request);
    }
}
