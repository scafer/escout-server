using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using escout.Helpers;
using escout.Models.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace escout.Services
{
    public class TokenService : BaseService
    {
        public static AuthData GenerateToken(User user)
        {
            var settings = Configurations.GetAppSettings().Build().GetSection("JwtSettings").Get<JwtSettings>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.SigningKey);
            var expirationTime = DateTime.UtcNow.AddMinutes(settings.ValidForMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthData
            {
                Token = tokenHandler.WriteToken(token),
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = user.id.ToString()
            };
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

    public static class UserExtensions
    {
        public static User GetUser(this ClaimsPrincipal claims, DataContext context)
        {
            var claimsIdentity = claims.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Name;
            var user = context.users.FirstOrDefault(u => u.id == int.Parse(userId));
            return user;
        }
    }
}
