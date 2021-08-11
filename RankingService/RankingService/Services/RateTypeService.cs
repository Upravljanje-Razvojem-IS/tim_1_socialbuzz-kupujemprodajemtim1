

using AutoMapper;
using RankingService.Data;
using RankingService.DTOs;
using RankingService.Entities;
using RankingService.Interfaces;
using RankingService.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RankingService.Services
{
    public class RateTypeService : IRateTypeService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public RateTypeService(IMapper mapper, DatabaseContext context, FakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public RateTypeConfirmationDto Create(RateTypeCreateDto dto)
        {
            RateType rateType = new RateType()
            {
                Id = Guid.NewGuid(),
                RateTypeName = dto.RateTypeName
            };

            _context.RateTypes.Add(rateType);
            _context.SaveChanges();

            _logger.Log("Create RateType");

            return _mapper.Map<RateTypeConfirmationDto>(rateType);
        }

        public void Delete(Guid id)
        {
            var rateType = _context.RateTypes.FirstOrDefault(e => e.Id == id);

            if (rateType != null)
            {
                _logger.Log("Delete RateType");

                _context.RateTypes.Remove(rateType);
            }
        }

        public List<RateTypeReadDto> Get()
        {
            var list = _context.RateTypes.ToList();

            _logger.Log("Get RateTypes");

            return _mapper.Map<List<RateTypeReadDto>>(list);
        }

        public RateTypeReadDto Get(Guid id)
        {
            var rateType = _context.RateTypes.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get RateType");

            return _mapper.Map<RateTypeReadDto>(rateType);
        }

        public RateTypeConfirmationDto Update(Guid id, RateTypeCreateDto dto)
        {
            var rate = _context.RateTypes.FirstOrDefault(e => e.Id == id);

            if (rate == null)
                return null;

            rate.RateTypeName = dto.RateTypeName;

            _context.SaveChanges();

            _logger.Log("Update RateType");

            return _mapper.Map<RateTypeConfirmationDto>(rate);
        }
    }
}
