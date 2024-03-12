using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Parkings.Secutiry
{
    public class Authentication : IAuthentication
    {
        private const int SaltSize = 24;
        private const int HashSize = 20;
        private const int Iterations = 10000;

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


        public string HasPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            string passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }


        public bool VerifyPassword(string password, string passwordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}