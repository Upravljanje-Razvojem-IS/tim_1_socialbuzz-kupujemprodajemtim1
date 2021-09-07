using System.ComponentModel.DataAnnotations;

namespace BlackListService.Dtos
{
    public class BlockCreateDto
    {
        /// <summary>
        /// Blocked user Id
        /// </summary>
        [Required]
        public int BlockedId { get; set; }

        /// <summary>
        /// Blocker user Id
        /// </summary>
        [Required]
        public int BlockerId { get; set; }

    }
}
