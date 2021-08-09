using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface IPostRateService
    {
        List<PostRateReadDTO> Get();
        PostRateReadDTO Get(Guid id);
        PostRateConfirmationDTO Create(PostRateCreateDTO dto);
        PostRateConfirmationDTO Update(Guid id, PostRateCreateDTO dto);
        void Delete(Guid id);
    }
}
