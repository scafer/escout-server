using escout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace escout.Services
{
    public class AuthenticationService
    {
        public User SignIn(User user)
        {
            var account = GetUser(user.username);

            if(account != null)
            {
                var isPasswordEquals = VerifyPassword(user.password, account.password);

                if (isPasswordEquals)
                {
                    return account;
                }
            }
            return null;
        }

        private User GetUser(string username)
        {
            throw new NotImplementedException();
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
