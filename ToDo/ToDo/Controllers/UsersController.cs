using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DTO;
using ToDo.Model;
using ToDo.Services.PasswordHash;
using ToDo.Services.TokenGenerator;
using ToDo.Services.User;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly BcryptPasswordHasher _passwordHasher;
        private readonly AccessTokenGenerator _accessTokenGenerator;

        public UsersController(IUserService respository, BcryptPasswordHasher passwordHasher,AccessTokenGenerator accessTokenGenerator)
        {
            _userService = respository;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            User existUserByEmail = await _userService.GetByEmail(registerRequest.Email);
            if (existUserByEmail != null)
            {
                return Conflict(new ErrorResponse("Email already exist"));
            }
            User existUserByUsername = await _userService.GetByUserName(registerRequest.Username);
            if (existUserByUsername != null)
            {
                return Conflict(new ErrorResponse("Username already exist"));
            }
            await _userService.SignUp(registerRequest);
            
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            User user = await _userService.GetByUserName(loginRequest.Username);
            if (user==null)
            {
                return BadRequest();
            }

            bool iscorrectPassword = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash);
            if (!iscorrectPassword)
            {
                return Unauthorized();
            }

            string accessToken = _accessTokenGenerator.GenerateToken(user);
            return Ok(new AuthenticateUserResponse()
            {
                AccessToken = accessToken
            });
        }

    }
}
