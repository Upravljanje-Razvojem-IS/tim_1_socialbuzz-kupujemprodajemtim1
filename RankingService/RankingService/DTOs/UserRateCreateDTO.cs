﻿using System;

namespace RankingService.DTOs
{
    public class UserRateCreateDto
    {
        public int RateScale { get; set; }
        public string Description { get; set; }
        public Guid RateTypeId { get; set; }
        public int UserId { get; set; }
    }
}
