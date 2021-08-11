using System.Collections.Generic;

namespace RankingService.Mocks
{
    public static class UserMock
    {
        public readonly static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "UserOne"
            },
            new User
            {
                Id = 2,
                Username = "UserTwo"
            }
        };
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
