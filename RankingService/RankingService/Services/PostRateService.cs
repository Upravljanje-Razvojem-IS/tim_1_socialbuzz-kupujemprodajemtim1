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
    public class PostRateService : IPostRateService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly FakeLogger _logger;

        public PostRateService(IMapper mapper, DatabaseContext context, FakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public PostRateConfirmationDTO Create(PostRateCreateDTO dto)
        {
            Post post = PostMock.Posts.FirstOrDefault(e => e.Id == dto.PostId);

            if (post == null)
                return null;

            PostRate newRate = new PostRate()
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                RateScale = dto.RateScale,
                RateTypeId = dto.RateTypeId,
                PostId = dto.PostId
            };

            _context.PostRate.Add(newRate);
            _context.SaveChanges();

            _logger.Log("Create PostRate");

            return _mapper.Map<PostRateConfirmationDTO>(newRate);
        }

        public void Delete(Guid id)
        {
            var rate = _context.PostRate.FirstOrDefault(e => e.Id == id);

            if (rate != null)
            {
                _logger.Log("Delete PostRate");
                _context.PostRate.Remove(rate);
            }
        }

        public List<PostRateReadDTO> Get()
        {
            var list = _context.PostRate.ToList();

            _logger.Log("Get PostRates");

            return _mapper.Map<List<PostRateReadDTO>>(list);
        }

        public PostRateReadDTO Get(Guid id)
        {
            var rate = _context.PostRate.FirstOrDefault(e => e.Id == id);

            _logger.Log("Get PostRate");

            return _mapper.Map<PostRateReadDTO>(rate);
        }

        public PostRateConfirmationDTO Update(Guid id, PostRateCreateDTO dto)
        {
            Post post = PostMock.Posts.FirstOrDefault(e => e.Id == dto.PostId);

            if (post == null)
                return null;

            var rate = _context.PostRate.FirstOrDefault(e => e.Id == id);

            if (rate == null)
                return null;

            rate.Description = dto.Description;
            rate.RateScale = dto.RateScale;
            rate.RateTypeId = dto.RateTypeId;
            rate.PostId = dto.PostId;

            _context.SaveChanges();

            _logger.Log("Update PostRate");

            return _mapper.Map<PostRateConfirmationDTO>(rate);
        }
    }
}
