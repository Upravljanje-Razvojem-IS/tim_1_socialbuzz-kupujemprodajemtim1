using AccountService.Data.Interfaces;
using AccountService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Data.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Email = "user@example.com",
                Password = "string"
            }
        };

        public User Get(string email)
        {
            User user = _users.FirstOrDefault(user => user.Email == email);

            return user;
        }

        public User Get(int id)
        {
            User user = _users.FirstOrDefault(user => user.Id == id);

            return user;
        }
    }
}
