using System.Collections.Generic;
using Visitor_Pass_Monitoring_System.Models;

namespace Visitor_Pass_Monitoring_System.Repositories
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "System",
                LastName = "Administrator",
                EmailAddress = "admin@visitor.com",
                Username = "admin",
                Password = "admin123"
            }
        };

        private static int _nextId = 2;

        public void AddUser(User user)
        {
            user.Id = _nextId;
            _nextId = _nextId + 1;
            _users.Add(user);
        }

        public User GetUserByCredentials(string username, string password)
        {
            foreach (User u in _users)
            {
                if (u.Username == username && u.Password == password)
                {
                    return u;
                }
            }

            return null;
        }
    }
}
