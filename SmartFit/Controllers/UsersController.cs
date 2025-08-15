using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Interfaces;
using SmartFit.Model;

namespace SmartFit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // 取得所有使用者
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users); // 泛型確保是 IEnumerable<UserDto>
        }

        // 取得單一使用者
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // 新增使用者
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // 更新使用者
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, User user)
        {
            if (id != user.Id) return BadRequest("Id mismatch.");
            await _userService.UpdateAsync(user);
            return NoContent(); // 沒內容回傳但狀態碼 204
        }

        // 刪除使用者
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
