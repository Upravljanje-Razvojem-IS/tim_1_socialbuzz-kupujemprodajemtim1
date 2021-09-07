using AutoMapper;
using BlackListService.Dtos;
using BlackListService.Entities;

namespace BlackListService.Profiles
{
    public class BlockMapper : Profile
    {
        public BlockMapper()
        {
            CreateMap<Block, BlockReadDto>();
            CreateMap<BlockCreateDto, Block>();
        }
    }
}
