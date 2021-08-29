using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.MockServices.BlackList
{
    public class BlackList
    {

        public Guid BlackListId { get; set; }

        public Guid BlockedId { get; set; }

        public Guid BlockerId { get; set; }
    }
}
