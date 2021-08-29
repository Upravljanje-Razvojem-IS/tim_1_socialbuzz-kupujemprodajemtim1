using AutoMapper;
using FinantialService.Entities;
using FinantialService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(
                dest => dest.DeliveryInfo,
                opt => opt.MapFrom(src => $"{ src.DeliveryAddress }, { src.DeliveryCity }"));

            CreateMap<TransactionCreateDto, Transaction>();
            CreateMap<TransactionUpdateDto, Transaction>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Ne mapira null vrednosti
        }

    }
}
