using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Interfaces;
using SmartFit.Model;

namespace SmartFit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IAuthService authService, IJwtService jwtService)
        {
            _userService = userService;
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister request)
        {
            try
            {
                await _userService.RegisterAsync(request);
                return Ok(new { message = "註冊成功" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _authService.AuthenticateAsync(request);
            if (user == null) return Unauthorized(new { message = "帳號或密碼錯誤" });

            var token = _jwtService.GenerateToken(user);

            return Ok(new LoginResponse
            {
                Token = token,
                Email = user.Email,
                Name = user.Name
            });
        }

        //[Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId)) return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();

            user.PasswordHash = "";
            return Ok(user);
        }
    }
}
