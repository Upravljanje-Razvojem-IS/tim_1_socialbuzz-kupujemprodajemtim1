using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface IRateTypeService
    {
        List<RateTypeReadDto> Get();
        RateTypeReadDto Get(Guid id);
        RateTypeConfirmationDto Create(RateTypeCreateDto dto);
        RateTypeConfirmationDto Update(Guid id, RateTypeCreateDto dto);
        void Delete(Guid id);
    }
}
