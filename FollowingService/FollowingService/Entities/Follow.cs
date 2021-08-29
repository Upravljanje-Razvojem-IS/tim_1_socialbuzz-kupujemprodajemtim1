using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Entities
{
    /// <summary>
    /// Represents follow
    /// </summary>
    public class Follow
    {
        /// <summary>
        /// Follow id
        /// </summary>
        [Required]
        public Guid FollowId { get; set; }

        /// <summary>
        /// Id of user who is followed
        /// </summary>
        [Required]
        public Guid FollowedId { get; set; }

        //// <summary>
        /// Id of user who is following
        /// </summary>
        [Required]
        public Guid FollowerId { get; set; }
    }
}
