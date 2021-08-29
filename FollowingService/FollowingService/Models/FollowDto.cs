using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Models
{

    /// <summary>
    /// DTO for Follow
    /// </summary>
    public class FollowDto
    {

        /// <summary>
        /// Follow id
        /// </summary>
        public Guid FollowId { get; set; }

        /// <summary>
        /// Id of followed user
        /// </summary>
        public Guid FollowedId { get; set; }

        /// <summary>
        /// Id of user who is following
        /// </summary>
        public Guid FollowerId { get; set; }
    }
}
