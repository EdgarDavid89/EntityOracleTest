using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Parkings.Secutiry
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;
        public Authentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(string user)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user));
            claims.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));
            claims.AddClaim(new Claim(ClaimTypes.Email, "someemail@google.com"));
            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credencialesToken
            };

            tokenDescriptor.Subject.AddClaim(new Claim($"Permission: Workers:Read", true ? "true" : "false"));
            tokenDescriptor.Subject.AddClaim(new Claim($"Permission: Wrokers:Write", false ? "true" : "false"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;

        }
    }
}