using escout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace escout.Services
{
    public class UserService
    {
        DataContext db;
        AuthenticationService auth;

        public UserService()
        {
            db = new DataContext();
            auth = new AuthenticationService();
        }

        public User GetUser(string username)
        {
            return db.users.Where(u => u.username == username).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return db.users.ToList<User>();
        }

        public User GetUserById(int id)
        {
            return db.users.Where(u => u.id == id).FirstOrDefault();
        }

        public string ResetPassword(string username, string email)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckEmailExist(string email)
        {
            throw new NotImplementedException();
        }

        public bool CheckUsernameExist(string username)
        {
            throw new NotImplementedException();
        }

        public bool CheckCredentials(string username, string email)
        {
            throw new NotImplementedException();
        }

        public bool SendEmailToUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
