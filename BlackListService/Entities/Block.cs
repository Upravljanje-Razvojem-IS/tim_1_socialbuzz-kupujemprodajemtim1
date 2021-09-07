using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlackListService.Entities
{
    public class Block
    {
        /// <summary>
        /// BlackList Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Blocked user Id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int BlockedId { get; set; }

        /// <summary>
        /// Blocker user Id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int BlockerId { get; set; }

        /// <summary>
        /// Block create date time
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
