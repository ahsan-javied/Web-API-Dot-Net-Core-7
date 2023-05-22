using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.User;
using Services.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthenticatedUserDTO _loggedInUser;
        public UsersController(IUserService userService, IHttpContextAccessor accessor)
        {
            _userService = userService;
            _loggedInUser = (AuthenticatedUserDTO)accessor?.HttpContext?.Items["User"];
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequestDTO request)
        {
            var result = await _userService.SignupAsync(request);

            if (result) return Ok(); // Return 200 OK if signup is successful

            return BadRequest();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDTO request)
        {
            // Call the appropriate service method to authenticate the user
            var user = await _userService.AuthenticateAsync(request);

            if (user == null)
                return Unauthorized(); // Return 401 Unauthorized if authentication fails

            // Generate a JWT token

            return new JsonResult(user);
        }


        [Helpers.FilterHandlers.Authorize]
        [HttpGet("auth/balance")]
        public async Task<IActionResult> GetBalance()
        {
            decimal balance = await _userService.GetUserBalanceAsync(_loggedInUser);

            return new JsonResult(new { balance })
            ;
        }
    }
}
