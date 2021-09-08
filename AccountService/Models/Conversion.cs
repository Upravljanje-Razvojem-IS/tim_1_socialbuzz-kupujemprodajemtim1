using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Models
{
    public class Conversion
    {
        /// <summary>
        /// Id of Conversion.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rate of conversion.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Exchange { get; set; }

        /// <summary>
        /// Id of the in currency.
        /// </summary>
        [Required]
        public int CurrencyInId { get; set; }

        /// <summary>
        /// Id of the out currency.
        /// </summary>
        [Required]
        public int CurrencyOutId { get; set; }

        public virtual Currency CurrencyIn { get; set; }

        public virtual Currency CurrencyOut { get; set; }

        /// <summary>
        /// DateTime when Conversion was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
