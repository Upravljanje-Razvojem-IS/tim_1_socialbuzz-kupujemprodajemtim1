using AccountService.Dtos;
using AccountService.Models;
using AutoMapper;

namespace AccountService.Profiles
{
    public class CurrencyMapper : Profile
    {
        public CurrencyMapper()
        {
            CreateMap<Currency, CurrencyReadDto>();
            CreateMap<CurrencyCreateDto, Currency>();
            CreateMap<CurrencyUpdateDto, Currency>();
        }
    }
}
