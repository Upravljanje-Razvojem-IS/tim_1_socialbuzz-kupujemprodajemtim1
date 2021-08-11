using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface IUserRateService
    {
        List<UserRateReadDto> Get();
        UserRateReadDto Get(Guid id);
        UserRateConfirmationDto Create(UserRateCreateDto dto);
        UserRateConfirmationDto Update(Guid id, UserRateCreateDto dto);
        void Delete(Guid id);
    }
}
