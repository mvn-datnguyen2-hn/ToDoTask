using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDo.DTOs;
using ToDo.Model;

namespace ToDo.Services.TokenGenerator
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;

        public AccessTokenGenerator(IOptions<AuthenticationConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        public string GenerateToken(UserResponse user)
        {
            
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.AccessTokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("sub", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),

            };
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration.Issuer,
                _configuration.Audience
                ,claims
                ,DateTime.UtcNow
                ,DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes)
                ,credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
