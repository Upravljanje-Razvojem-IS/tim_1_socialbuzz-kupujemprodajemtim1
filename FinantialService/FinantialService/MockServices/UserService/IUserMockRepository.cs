using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.UserService
{
    public interface IUserMockRepository
    {
        PersonalAccount GetAccounByUserId(Guid userId);
    }
}
