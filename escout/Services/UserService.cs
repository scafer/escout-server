using escout.Helpers;
using escout.Models;
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

        public User CreateUser(User user)
        {
            db.users.Add(user);
            db.SaveChanges();
            return user;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                db.users.Update(user);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveUser(User user)
        {
            try
            {
                db.users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool ResetPassword(string username, string email)
        {
            var user = db.users.FirstOrDefault(u => u.username == username || u.email == email);

            if (user != null)
            {
                var generatedPassword = Utils.StringGenerator();
                user.password = new AuthService().HashPassword(Utils.GenerateSha256String(generatedPassword));
                db.users.Update(user);
                db.SaveChanges();
                NotificationHelper.SendEmail(user.email, "New eScout Password", generatedPassword);
                return true;
            }

            return false;
        }

        public bool ChangePassword(User user, string newPassword)
        {
            try
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                db.users.Update(user);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public User GetUser(int id)
        {
            return db.users.FirstOrDefault(u => u.id == id);
        }

        public User GetUser(string username)
        {
            return db.users.FirstOrDefault(u => u.username == username);
        }

        public List<User> GetUsers()
        {
            return db.users.ToList();
        }

        public bool CheckEmailExist(string email)
        {
            var check = db.users.FirstOrDefault(u => u.email == email);
            return check != null;
        }

        public bool CheckUsernameExist(string username)
        {
            var check = db.users.FirstOrDefault(u => u.username == username);
            return check != null;
        }

        public bool CheckCredentials(string username, string email)
        {
            var check = db.users.FirstOrDefault(u => u.username == username || u.email == email);
            return check != null;
        }
    }
}