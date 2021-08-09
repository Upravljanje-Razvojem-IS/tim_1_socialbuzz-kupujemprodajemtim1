using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RankingService.Entities
{
    public class RateType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string RateTypeName { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
