using System;

namespace RankingService.DTOs
{
    public class TransportRateCreateDto
    {
        public int RateScale { get; set; }
        public string Description { get; set; }
        public Guid RateTypeId { get; set; }
        public int TransportId { get; set; }
    }
}
