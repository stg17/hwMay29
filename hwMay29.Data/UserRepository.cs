using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwMay29.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new TaskDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return null;
            }
            return user;
        }

        public User GetByEmail(string email)
        {
            var context = new TaskDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => email == u.Email);
        }
    }
}
