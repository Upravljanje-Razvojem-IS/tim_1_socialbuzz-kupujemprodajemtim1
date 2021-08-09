using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface IRateTypeService
    {
        List<RateTypeReadDTO> Get();
        RateTypeReadDTO Get(Guid id);
        RateTypeConfirmationDTO Create(RateTypeCreateDTO dto);
        RateTypeConfirmationDTO Update(Guid id, RateTypeCreateDTO dto);
        void Delete(Guid id);
    }
}
