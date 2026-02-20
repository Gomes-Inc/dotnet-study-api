using app_test_api.Models.Request;
using app_test_api.Models.Response;
using app_test_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app_test_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService usersService)
        {
            _userService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDetailResponse>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDetailResponse>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Create([FromBody] CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { message = "Name is required" });
            }

            if (request.Name.Length > 100)
            {
                return BadRequest(new { message = "Name must be 100 characters or less" });
            }

            var user = await _userService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid user ID" });
            }

            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            return NoContent();
        }
    }
}
