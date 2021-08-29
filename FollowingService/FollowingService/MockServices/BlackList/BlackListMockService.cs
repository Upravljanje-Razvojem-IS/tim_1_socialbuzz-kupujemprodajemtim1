using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.MockServices.BlackList
{
    public class BlackListMockService : IBlackListMockRepository
    {
        public static List<BlackList> BlackLists { get; set; } = new List<BlackList>()
        {
                new BlackList
                {
                   BlackListId = Guid.Parse("3c96df74-103a-47c8-a563-12cbb056bcbc"),
                   BlockedId = Guid.Parse("5e66446a-166d-40b1-882d-04f60f5d4280"),
                   BlockerId = Guid.Parse("006e4342-b705-44a1-b644-6d03d40d62a0")
                },
                new BlackList
                {
                   BlackListId = Guid.Parse("9ed77d09-ebb8-4960-b26a-c6ae32037e1a"),
                   BlockedId = Guid.Parse("006e4342-b705-44a1-b644-6d03d40d62a0"),
                   BlockerId = Guid.Parse("695af082-9ff5-441d-95e1-066668ce94ad"),
                }


        };

        public bool IsUserBlocked(Guid blockerId, Guid blockedId)
        {
            foreach (var u in BlackLists)
            {
                if (u.BlockerId == blockerId && u.BlockedId == blockedId)
                {
                    return true;
                }
                else if (u.BlockerId == blockedId && u.BlockedId == blockerId)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
