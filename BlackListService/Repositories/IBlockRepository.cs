using BlackListService.Entities;
using System.Collections.Generic;

namespace BlackListService.Repositories
{
    public interface IBlockRepository
    {
        IEnumerable<Block> Get();
        Block Get(int id);
        Block Get(int blockerId, int blockedId);
        void Create(Block blackList);
        void Delete(Block blackList);
    }
}
