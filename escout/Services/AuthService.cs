using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Mvc;
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
            var account = agent.GetUser(user.username);

            if (account == null) return null;
            return VerifyPassword(user.password, account.password) ? account : null;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        internal bool SignUp(User user)
        {
            try
            {
                user.password = HashPassword(user.password);
                var userService = new UserService();
                userService.CreateUser(user);
                NotificationHelper.SendEmail(user.email, "Welcome to eScout", "Welcome to eScout " + user.username);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}