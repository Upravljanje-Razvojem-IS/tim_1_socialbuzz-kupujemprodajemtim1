using AutoMapper;
using FinantialService.Data.Interfaces;
using FinantialService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionContext transactionContext;
        private readonly IMapper mapper;

        public TransactionRepository(TransactionContext transactionContext, IMapper mapper)
        {
            this.transactionContext = transactionContext;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return transactionContext.SaveChanges() > 0;
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            var created = transactionContext.Transactions.Add(transaction);
            return mapper.Map<Transaction>(created.Entity);
        }

        public List<Transaction> GetTransactions(Guid buyerId, string deliveryAddress = null, string deliveryCity = null)
        {
            return transactionContext.Transactions.Where(t =>
                                                        (deliveryAddress == null || t.DeliveryAddress == deliveryAddress) &&
                                                        (deliveryCity == null || t.DeliveryCity == deliveryCity) &&
                                                        (buyerId == Guid.Empty || t.BuyerId == buyerId)).ToList();

        }

        public Transaction GetTransactionById(Guid transactionId)
        {
            return transactionContext.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }


        public void DeleteTransaction(Guid transactionId)
        {
            var transaction = GetTransactionById(transactionId);
            transactionContext.Remove(transaction);
        }


    }
}
