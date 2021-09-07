using System;
using System.ComponentModel.DataAnnotations;

namespace BlackListService.Dtos
{
    public class BlockReadDto
    {
        /// <summary>
        /// BlackList Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Blocked user Id
        /// </summary>
        public int BlockedId { get; set; }

        /// <summary>
        /// Blocker user Id
        /// </summary>
        public int BlockerId { get; set; }

        /// <summary>
        /// Block create date time
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
