using System;

namespace RankingService.DTOs
{
    public class PostRateConfirmationDTO
    {
        public Guid Id { get; set; }
        public int RateScale { get; set; }
        public string Description { get; set; }
        public Guid RateTypeId { get; set; }
        public int PostId { get; set; }
    }
}
