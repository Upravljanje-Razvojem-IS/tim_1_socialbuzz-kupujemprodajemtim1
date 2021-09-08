using System.ComponentModel.DataAnnotations;

namespace AccountService.Dtos
{
    public class AccountCreateDto
    {
        /// <summary>
        /// Users id.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Currency id.
        /// </summary>
        [Required]
        public int CurrencyId { get; set; }

        /// <summary>
        /// Balance of the account.
        /// </summary>
        public decimal Total { get; set; } = 0;
    }
}
