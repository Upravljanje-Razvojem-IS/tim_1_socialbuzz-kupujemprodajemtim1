using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface ITransportRateService
    {
        List<TransportRateReadDTO> Get();
        TransportRateReadDTO Get(Guid id);
        TransportRateConfirmationDTO Create(TransportRateCreateDTO dto);
        TransportRateConfirmationDTO Update(Guid id, TransportRateCreateDTO dto);
        void Delete(Guid id);
    }
}
