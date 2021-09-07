using BlackListService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlackListService.Context
{
    public class BlockContext : DbContext
    {
        public DbSet<Block> Blocks { get; set; }

        public BlockContext(DbContextOptions<BlockContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>()
                .HasIndex(p => new { p.BlockedId, p.BlockerId })
                .IsUnique();
        }
    }
}
