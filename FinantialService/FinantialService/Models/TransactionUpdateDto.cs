using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Models
{
    /// <summary>
    /// DTO for updating a transaction
    /// </summary>
    public class TransactionUpdateDto
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [Required]
        public Guid TransactionId { get; set; }

        /// <summary>
        /// The address to which the order is delivered
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// The city where the order is delivered
        /// </summary>
        public string DeliveryCity { get; set; }
    }
}
