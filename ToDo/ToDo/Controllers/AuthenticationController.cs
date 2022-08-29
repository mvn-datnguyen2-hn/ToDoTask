using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Model;
using ToDo.Model.Request;
using ToDo.Model.Response;
using ToDo.Services.PasswordHash;
using ToDo.Services.TokenGenerator;
using ToDo.Services.User;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRespository _userRespository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AccessTokenGenerator _accessTokenGenerator;

        public AuthenticationController(IUserRespository respository, IPasswordHasher passwordHasher,AccessTokenGenerator accessTokenGenerator)
        {
            _userRespository = respository;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            if (registerRequest.Password!=registerRequest.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("Password doesnt match"));
            }

            User existUserByEmail = await _userRespository.GetByEmail(registerRequest.Email);
            if (existUserByEmail!=null)
            {
                return Conflict(new ErrorResponse("Email already exist"));
            }
            User existUserByUsername = await _userRespository.GetByUserName(registerRequest.Username);
            if (existUserByUsername != null)
            {
                return Conflict(new ErrorResponse("Username already exist"));
            }

            string passwordHash = _passwordHasher.HashPassword(registerRequest.Password);
            User registrationUser = new User()
            {
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                PasswordHash = passwordHash
            };
            await _userRespository.CreateUser(registrationUser);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            User user = await _userRespository.GetByUserName(loginRequest.Username);
            if (user==null)
            {
                return Unauthorized();
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

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessage =
                ModelState.Values.SelectMany(c => c.Errors.Select(c => c.ErrorMessage));
            return BadRequest(new ErrorResponse(errorMessage));
        }
    }
}
