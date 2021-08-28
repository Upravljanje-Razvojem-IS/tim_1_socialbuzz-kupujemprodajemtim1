using FinantialService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.AccountService
{
    public class AccountMockService : IAccountMockRepository
    {
        public static List<Account> PersonalAccounts { get; set; } = new List<Account>()
        {
                new Account
                {
                   AccountId = Guid.Parse("412d28bc-fc00-4598-8a92-7a555d33e33d"),
                   AccountBalance =  80000
                },
                new Account
                {
                   AccountId = Guid.Parse("321db618-29a4-4289-816c-2d40767758fd"),
                   AccountBalance =  3000
                }


        };

        public bool charge(TransactionChargeDto charge)
        {
            Account account = PersonalAccounts.FirstOrDefault(p => p.AccountId == charge.AccountId);
            if (account != null && account.AccountBalance >= charge.Amount)
            {
                account.AccountBalance -= charge.Amount;
                return true;
            }
            return false;
        }

        public Account getAccountById(Guid accountId)
        {
            Account account = PersonalAccounts.FirstOrDefault(p => p.AccountId == accountId);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public bool refund(TransactionChargeDto refund)
        {
            Account account = PersonalAccounts.FirstOrDefault(p => p.AccountId == refund.AccountId);
            if (account != null)
            {
                account.AccountBalance += refund.Amount;
                return true;
            }
            return false;
        }
    }
}
