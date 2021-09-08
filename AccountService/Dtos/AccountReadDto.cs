using System;

namespace AccountService.Dtos
{
    public class AccountReadDto
    {
        /// <summary>
        /// Id of an Account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Currency Id.
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Balance of an Account.
        /// </summary>
        public decimal Total { get; set; }

        public CurrencyReadDto Currency { get; set; }

        /// <summary>
        /// DateTime when an Account was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
