using escout.Helpers;
using escout.Models;
using System;

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
}