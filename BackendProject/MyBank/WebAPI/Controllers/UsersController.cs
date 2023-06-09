﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Common;
using Models.Domain.Entites;
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
        public async Task<APIResponse> Signup([FromBody] SignupRequestDTO request)
        {
            var result = await _userService.SignupAsync(request);

            // Return 200 OK if signup is successful
            if (result) return new APIResponse(true, StatusCodes.Status200OK, "Success", "Success");

            return new APIResponse(false, StatusCodes.Status400BadRequest, "BadRequest", null);
        }

        [HttpPost("authenticate")]
        public async Task<APIResponse> Authenticate([FromBody] AuthenticateRequestDTO request)
        {
            // Call the appropriate service method to authenticate the user
            var user = await _userService.AuthenticateAsync(request);

            // Return 401 Unauthorized if authentication fails
            if (user == null)
                return new APIResponse(false, StatusCodes.Status401Unauthorized, "Unauthorized", null);

            // Generate a JWT token
            return new APIResponse(true, StatusCodes.Status201Created, "Success", user);
        }


        [Helpers.FilterHandlers.Authorize]
        [HttpGet("auth/balance")]
        public async Task<APIResponse> GetBalance()
        {
            decimal balance = await _userService.GetUserBalanceAsync(_loggedInUser);

            return new APIResponse(true, StatusCodes.Status201Created, "Success", balance);
        }
    }
}
