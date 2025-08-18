using SmartFit.Model;

namespace SmartFit.Interfaces
{
    public interface IAuthService
    {
        Task<UserRegister?> AuthenticateAsync(UserLoginRequest request);
    }
}
