using System;

namespace RankingService.DTOs
{
    public class TransportRateConfirmationDto
    {
        public Guid Id { get; set; }
        public int RateScale { get; set; }
        public string Description { get; set; }
        public Guid RateTypeId { get; set; }
        public int TransportId { get; set; }
    }
}
