using System;
using System.ComponentModel.DataAnnotations;

namespace AccountService.Models
{
    public class Currency
    {
        /// <summary>
        /// Id of Currency.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of a Currency.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// DateTime when Currency was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}