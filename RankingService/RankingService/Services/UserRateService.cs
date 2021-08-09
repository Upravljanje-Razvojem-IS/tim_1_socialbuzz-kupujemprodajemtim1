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
    public class UserRateService : IUserRateService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public UserRateService(IMapper mapper, DatabaseContext context, FakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public UserRateConfirmationDTO Create(UserRateCreateDTO dto)
        {
            User transport = UserMock.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (transport == null)
                return null;

            UserRate newRate = new UserRate()
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                RateScale = dto.RateScale,
                RateTypeId = dto.RateTypeId,
                UserId = dto.UserId
            };

            _context.UserRate.Add(newRate);
            _context.SaveChanges();

            _logger.Log("Create UserRate");

            return _mapper.Map<UserRateConfirmationDTO>(newRate);
        }

        public void Delete(Guid id)
        {
            var rate = _context.UserRate.FirstOrDefault(e => e.Id == id);

            if (rate != null)
            {
                _context.UserRate.Remove(rate);

                _logger.Log("Delete UserRate");
            }
        }

        public List<UserRateReadDTO> Get()
        {
            var list = _context.UserRate.ToList();


            _logger.Log("Get UserRates");

            return _mapper.Map<List<UserRateReadDTO>>(list);
        }

        public UserRateReadDTO Get(Guid id)
        {
            var rate = _context.UserRate.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get UserRate");

            return _mapper.Map<UserRateReadDTO>(rate);
        }

        public UserRateConfirmationDTO Update(Guid id, UserRateCreateDTO dto)
        {
            User transport = UserMock.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (transport == null)
                return null;

            var rate = _context.UserRate.FirstOrDefault(e => e.Id == id);

            if (rate == null)
                return null;

            rate.Description = dto.Description;
            rate.RateScale = dto.RateScale;
            rate.RateTypeId = dto.RateTypeId;
            rate.UserId = dto.UserId;

            _context.SaveChanges();

            _logger.Log("Update UserRate");

            return _mapper.Map<UserRateConfirmationDTO>(rate);
        }
    }
}
