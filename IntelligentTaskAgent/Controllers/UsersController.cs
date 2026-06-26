using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Entities;

namespace IntelligentTaskAgent.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var userId = await userService.CreateAsync(request.Name);
            return Ok(new { UserId = userId });
        }
    }
}
