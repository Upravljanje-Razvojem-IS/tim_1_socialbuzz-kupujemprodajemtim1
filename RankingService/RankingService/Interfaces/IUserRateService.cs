using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface IUserRateService
    {
        List<UserRateReadDTO> Get();
        UserRateReadDTO Get(Guid id);
        UserRateConfirmationDTO Create(UserRateCreateDTO dto);
        UserRateConfirmationDTO Update(Guid id, UserRateCreateDTO dto);
        void Delete(Guid id);
    }
}
