using app_test_api.Models;
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
        public async Task<ActionResult<List<Message>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
    }
}
