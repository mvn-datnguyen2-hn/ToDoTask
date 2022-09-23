using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DTOs;
using ToDo.Models;
using ToDo.Services.PasswordHash;
using ToDo.Services.TokenGenerator;
using ToDo.Services.Users;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        private readonly AccessTokenGenerator _accessTokenGenerator;

        public UsersController(IUserService userService, AccessTokenGenerator accessTokenGenerator)
        {
            _userService = userService;
            _accessTokenGenerator = accessTokenGenerator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            UserResponse existUserByEmail = await _userService.GetByEmail(registerRequest.Email);
            if (existUserByEmail != null)
            {
                return BadRequest(new ErrorResponse("Email already exist"));
            }
            UserResponse existUserByUsername = await _userService.GetByUserName(registerRequest.Username);
            if (existUserByUsername != null)
            {
                return BadRequest(new ErrorResponse("Username already exist"));
            }
            await _userService.SignUp(registerRequest);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            UserResponse user = await _userService.GetByUserName(loginRequest.Username);
            if (user == null)
            {
                return BadRequest();
            }

            if (!_userService.CheckLogin(loginRequest))
            {
                return BadRequest();
            }

            string accessToken = _accessTokenGenerator.GenerateToken(user);
            return Ok(new AuthenticateUserResponse()
            {
                AccessToken = accessToken
            });
        }

    }
}
