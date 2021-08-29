using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Models
{
    /// <summary>
    /// DTO for creating a follow
    /// </summary>
    public class FollowCreateDto
    {

        /// <summary>
        /// Id of user who is followed
        /// </summary>
        [Required]
        public Guid FollowedId { get; set; }

        //// <summary>
        /// Id of user who is followinig
        /// </summary>
        [Required]
        public Guid FollowerId { get; set; }
    }
}
