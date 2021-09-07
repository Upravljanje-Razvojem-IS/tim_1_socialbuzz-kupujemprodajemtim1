using BlackListService.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BlackListService.Services
{
    public class FakeUserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1 },
            new User { Id = 2 },
            new User { Id = 3 },
            new User { Id = 4 },
            new User { Id = 5 },
        };

        public User Get(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}
