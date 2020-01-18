using escout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class UserService : BaseService
    {
        DataContext db;

        public UserService()
        {
            db = new DataContext();
        }

        public void AddUser(User usr)
        {
            var user = new User { username = usr.username, password = usr.password, email = usr.email };
            db.users.Add(user);
            db.SaveChanges();
        }

        public User GetUser(string username)
        {
            var user = db.users.FirstOrDefault(u => u.username == username);
            Console.WriteLine(user.ToString());
            return user;
        }

        public List<User> GetUsers()
        {
            return db.users.ToList<User>();
        }

        public User GetUserById(int id)
        {
            return db.users.FirstOrDefault(u => u.id == id);
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
            var check = db.users.FirstOrDefault(u => u.email == email);
            return check != null ? true : false;
        }

        public bool CheckUsernameExist(string username)
        {
            var check = db.users.FirstOrDefault(u => u.username == username);
            return check != null ? true : false;
        }

        public bool CheckCredentials(string username, string email)
        {
            var check = db.users.FirstOrDefault(u => u.username == username || u.email == email);
            return check != null ? true : false;
        }

        public bool SendEmailToUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
