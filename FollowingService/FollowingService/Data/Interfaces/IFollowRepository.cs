using FollowingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Data.Interfaces
{
    public interface IFollowRepository
    {
        List<Follow> GetFollows(Guid followerId);

        bool SaveChanges();

        Follow GetFollowById(Guid followId);

        Follow CreateFollow(Follow follow);

        void DeleteFollow(Guid followId);
    }
}
