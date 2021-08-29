using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.MockServices.BlackList
{
    public interface IBlackListMockRepository
    {
        bool IsUserBlocked(Guid blockerId, Guid blockedId);
    }
}
