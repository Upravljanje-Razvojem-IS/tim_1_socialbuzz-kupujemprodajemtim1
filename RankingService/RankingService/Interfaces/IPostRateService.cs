using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface IPostRateService
    {
        List<PostRateReadDto> Get();
        PostRateReadDto Get(Guid id);
        PostRateConfirmationDto Create(PostRateCreateDto dto);
        PostRateConfirmationDto Update(Guid id, PostRateCreateDto dto);
        void Delete(Guid id);
    }
}
