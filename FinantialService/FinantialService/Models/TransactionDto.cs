using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Models
{

    /// <summary>
    /// DTO for Transaction
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Buyer id
        /// </summary>
        public Guid BuyerId { get; set; }

        /// <summary>
        /// Product id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Transport type id
        /// </summary>
        public Guid TransportTypeId { get; set; }

        /// <summary>
        /// Date end time of order
        /// </summary>
        public DateTime BuyingDateTime { get; set; }

        /// <summary>
        /// Quantity of ordered products
        /// </summary>
        public int ProductsQuantity { get; set; }

        /// <summary>
        /// The address and city where the order is delivered
        /// </summary>
        public string DeliveryInfo { get; set; }
    }
}
