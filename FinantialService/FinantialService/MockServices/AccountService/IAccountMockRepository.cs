using FinantialService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.AccountService
{
    public interface IAccountMockRepository
    {
        Account getAccountById(Guid accountId);

        bool charge(TransactionChargeDto charge);

        bool refund(TransactionChargeDto refund);
    }
}
