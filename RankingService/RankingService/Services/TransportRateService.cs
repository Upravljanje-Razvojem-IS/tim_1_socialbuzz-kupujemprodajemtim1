using AutoMapper;
using RankingService.Data;
using RankingService.DTOs;
using RankingService.Entities;
using RankingService.Interfaces;
using RankingService.Logger;
using RankingService.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RankingService.Services
{
    public class TransportRateService : ITransportRateService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public TransportRateService(IMapper mapper, DatabaseContext context, FakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public TransportRateConfirmationDto Create(TransportRateCreateDto dto)
        {
            Transport transport = TransportMock.Transports.FirstOrDefault(e => e.Id == dto.TransportId);

            if (transport == null)
                return null;

            TransportRate newRate = new TransportRate()
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                RateScale = dto.RateScale,
                RateTypeId = dto.RateTypeId,
                TransportId = dto.TransportId
            };

            _context.TransportRate.Add(newRate);
            _context.SaveChanges();

            _logger.Log("Create TransportRate");

            return _mapper.Map<TransportRateConfirmationDto>(newRate);
        }

        public void Delete(Guid id)
        {
            var rate = _context.TransportRate.FirstOrDefault(e => e.Id == id);

            if (rate != null)
            {
                _context.TransportRate.Remove(rate);

                _logger.Log("Delete TransportRate");
            }
        }

        public List<TransportRateReadDto> Get()
        {
            var list = _context.TransportRate.ToList();

            _logger.Log("Get TransportRates");

            return _mapper.Map<List<TransportRateReadDto>>(list);
        }

        public TransportRateReadDto Get(Guid id)
        {
            var rate = _context.TransportRate.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get TransportRate");

            return _mapper.Map<TransportRateReadDto>(rate);
        }

        public TransportRateConfirmationDto Update(Guid id, TransportRateCreateDto dto)
        {
            Transport transport = TransportMock.Transports.FirstOrDefault(e => e.Id == dto.TransportId);

            if (transport == null)
                return null;

            var rate = _context.TransportRate.FirstOrDefault(e => e.Id == id);

            if (rate == null)
                return null;

            rate.Description = dto.Description;
            rate.RateScale = dto.RateScale;
            rate.RateTypeId = dto.RateTypeId;
            rate.TransportId = dto.TransportId;

            _context.SaveChanges();

            _logger.Log("Update TransportRate");

            return _mapper.Map<TransportRateConfirmationDto>(rate);
        }
    }
}
