using AutoMapper;
using FollowingService.Entities;
using FollowingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Profiles
{
    public class FollowProfile : Profile
    {
        public FollowProfile()
        {
            CreateMap<Follow, FollowDto>();
            CreateMap<FollowCreateDto, Follow>();
        }
    }
}
