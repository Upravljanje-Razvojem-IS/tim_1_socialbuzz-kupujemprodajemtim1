using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.MockServices.UserService
{
    public interface IUserMockRepository
    {
        PersonalAccount GetAccounByUserId(Guid userId);
    }
}
