using System;

namespace AccountService.Dtos
{
    public class CurrencyReadDto
    {

        /// <summary>
        /// Id of a Currency.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Name of a Currency.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DateTime when a Currency was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
