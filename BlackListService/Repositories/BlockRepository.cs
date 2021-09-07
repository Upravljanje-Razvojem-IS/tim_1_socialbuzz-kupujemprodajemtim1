using BlackListService.Context;
using BlackListService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlackListService.Repositories.Implementations
{
    public class BlockRepository : IBlockRepository
    {
        private readonly BlockContext _context;

        public BlockRepository(BlockContext context)
        {
            _context = context;
        }

        public void Create(Block blackList)
        {
            _context.Blocks.Add(blackList);
            _context.SaveChanges();   
        }

        public Block Get(int id)
        {
            return _context.Blocks
               .Where(e => e.Id == id)
               .FirstOrDefault();
        }

        public Block Get(int blockerId, int blockedId)
        {
            return _context.Blocks
               .Where(e => e.BlockerId== blockerId && e.BlockedId == blockedId)
               .FirstOrDefault();
        }

        public IEnumerable<Block> Get()
        {
            return _context.Blocks;
        }

        public void Delete(Block blackList)
        {
            _context.Blocks.Remove(blackList);
            _context.SaveChanges();
        }
    }
}
