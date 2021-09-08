using AccountService.Dtos;
using AccountService.Models;
using AutoMapper;

namespace AccountService.Profiles
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, AccountReadDto>();
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
        }
    }
}
