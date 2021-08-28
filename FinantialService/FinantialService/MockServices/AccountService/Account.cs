using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.AccountService
{
    public class Account
    {
        public Guid AccountId { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
