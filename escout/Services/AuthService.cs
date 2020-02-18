using escout.Helpers;
using escout.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace escout.Services
{
    public class AuthService : BaseService
    {
        public User SignIn(User user)
        {
            using var agent = new UserService();
            var account = agent.GetUser(user.username.ToLower());

            if (account == null) return null;
            return VerifyPassword(user.password, account.password) ? account : null;
        }

        internal bool SignUp(User user)
        {
            try
            {
                user.accessLevel = 0;
                user.created = Configurations.GetDateTime();
                user.updated = Configurations.GetDateTime();
                user.username = user.username.ToLower();
                user.email = user.email.ToLower();
                user.password = HashPassword(user.password);

                using var userService = new UserService();
                userService.CreateUser(user);

                if (user.notifications == 1)
                    NotificationHelper.SendEmail(user.email, "Welcome to eScout", "Welcome to eScout " + user.username);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

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
    }

    public static class UserExtensions
    {
        public static User GetUser(this ClaimsPrincipal claims)
        {
            using var agent = new UserService();
            var claimsIdentity = claims.Identity as ClaimsIdentity;
            var userId = claimsIdentity.Name;

            var user = agent.GetUser(int.Parse(userId));

            return user;
        }
    }
}