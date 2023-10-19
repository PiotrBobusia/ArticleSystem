using ArticleSystem.Entity;
using ArticleSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArticleSystem.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private JwtConfiguration _jwtConfiguration;

        public TokenGenerator(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }
        public string generateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.Now.AddDays(_jwtConfiguration.JwtExpiredDays);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("Birth", $"{user.DateOfBirth:dd/MM/yyyy}")
            };

            var token = new JwtSecurityToken(_jwtConfiguration.JwtIssuer,
                                            _jwtConfiguration.JwtIssuer,
                                            claims,
                                            expires: exp,
                                            signingCredentials: cred
                                            );
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
