using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.UserService
{
    public class UserMockService : IUserMockRepository
    {
        public static List<PersonalAccount> PersonalAccounts { get; set; } = new List<PersonalAccount>()
        {
                new PersonalAccount
                {
                   PersonalAccountId = Guid.Parse("006e4342-b705-44a1-b644-6d03d40d62a0"),
                   AccountId =  Guid.Parse("412d28bc-fc00-4598-8a92-7a555d33e33d")
                },
                new PersonalAccount
                {
                   PersonalAccountId = Guid.Parse("5e66446a-166d-40b1-882d-04f60f5d4280"),
                   AccountId =  Guid.Parse("321db618-29a4-4289-816c-2d40767758fd")
                }


        };

        public PersonalAccount GetAccounByUserId(Guid userId)
        {
            PersonalAccount account = PersonalAccounts.FirstOrDefault(p => p.PersonalAccountId == userId);
            if (account == null)
            {
                return null;
            }
            return account;
        }
    }
}
