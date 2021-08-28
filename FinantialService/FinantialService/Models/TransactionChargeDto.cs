using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Models
{
    public class TransactionChargeDto
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
