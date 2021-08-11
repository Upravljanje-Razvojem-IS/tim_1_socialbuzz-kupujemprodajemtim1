using System.Collections.Generic;

namespace RankingService.Mocks
{
    public static class PostMock
    {
        public readonly static List<Post> Posts = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                Name = "Post1"
            },
            new Post()
            {
                Id = 2,
                Name = "Post2"
            }
        };
    }

    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
