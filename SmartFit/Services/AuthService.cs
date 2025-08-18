using SmartFit.Interfaces;
using SmartFit.Model;

namespace SmartFit.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserRegister?> AuthenticateAsync(UserLoginRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Email);
            if (user == null) return null;

            return BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) ? user : null;
        }
    }
}
