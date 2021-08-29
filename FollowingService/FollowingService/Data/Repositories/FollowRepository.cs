using AutoMapper;
using FollowingService.Data.Interfaces;
using FollowingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Data.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly FollowingContext followingContext;
        private readonly IMapper mapper;

        public FollowRepository(FollowingContext followingContext, IMapper mapper)
        {
            this.followingContext = followingContext;
            this.mapper = mapper;
        }

        public Follow CreateFollow(Follow follow)
        {
            var created = followingContext.Add(follow);
            return mapper.Map<Follow>(created.Entity);
        }

        public List<Follow> GetFollows(Guid followerId)
        {
            return followingContext.Follows.Where(f => (followerId == Guid.Empty || f.FollowerId == followerId)).ToList();


        }

        public Follow GetFollowById(Guid followId)
        {
            return followingContext.Follows.FirstOrDefault(f => f.FollowId == followId);
        }

        public void DeleteFollow(Guid followId)
        {
            var follow = GetFollowById(followId);
            followingContext.Remove(follow);
        }

        public bool SaveChanges()
        {
            return followingContext.SaveChanges() > 0;
        }
    }
}
