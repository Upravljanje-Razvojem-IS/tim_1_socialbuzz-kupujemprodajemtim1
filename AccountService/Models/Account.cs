using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Models
{
    public class Account
    {
        /// <summary>
        /// Account id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of a User who owns this account.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of the accounts Currency.
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Accounts balance.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        public Currency Currency { get; set; }


        /// <summary>
        /// DateTime when this Account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
