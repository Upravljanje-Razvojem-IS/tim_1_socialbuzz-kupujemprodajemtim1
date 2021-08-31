using FinantialService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Data.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions(Guid buyerId, string deliveryAddress = null, string deliveryCity = null);

        bool SaveChanges();

        Transaction GetTransactionById(Guid transactionId);

        Transaction CreateTransaction(Transaction transaction);

        void DeleteTransaction(Guid transactionId);
    }
}
