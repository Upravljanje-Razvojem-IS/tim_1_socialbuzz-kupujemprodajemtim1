using RankingService.DTOs;
using System;
using System.Collections.Generic;

namespace RankingService.Interfaces
{
    public interface ITransportRateService
    {
        List<TransportRateReadDto> Get();
        TransportRateReadDto Get(Guid id);
        TransportRateConfirmationDto Create(TransportRateCreateDto dto);
        TransportRateConfirmationDto Update(Guid id, TransportRateCreateDto dto);
        void Delete(Guid id);
    }
}
