using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankingService.Entities
{
    public class Rate
    {
        [Key]
        public Guid Id { get; set; }
        [Range(1, 10)]
        [Required]
        public int RateScale { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ForeignKey("RateType")]
        public Guid RateTypeId { get; set; }
        public RateType RateType { get; set; }
    }
}
