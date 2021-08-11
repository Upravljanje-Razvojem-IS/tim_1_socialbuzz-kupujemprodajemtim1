using System;

namespace RankingService.DTOs
{
    public class PostRateCreateDto
    {
        public int RateScale { get; set; }
        public string Description { get; set; }
        public Guid RateTypeId { get; set; }
        public int PostId { get; set; }
    }
}
