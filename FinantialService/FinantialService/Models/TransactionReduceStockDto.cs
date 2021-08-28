using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Models
{
    public class TransactionReduceStockDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
