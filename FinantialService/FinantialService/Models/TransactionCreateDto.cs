using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Models
{
    /// <summary>
    /// DTO for creating a transaction
    /// </summary>
    public class TransactionCreateDto
    {
        /// <summary>
        /// Buyer id
        /// </summary>
        [Required]
        public Guid BuyerId { get; set; }

        /// <summary>
        /// Product id
        /// </summary>
        [Required]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Transport type id
        /// </summary>
        [Required]
        public Guid TransportTypeId { get; set; }

        /// <summary>
        /// Quantity of ordered products
        /// </summary>
        [Required]
        public int ProductsQuantity { get; set; }

        /// <summary>
        /// The address to which the order is delivered
        /// </summary>
        [Required]
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// The city where the order is delivered
        /// </summary>
        [Required]
        public string DeliveryCity { get; set; }


    }
}
