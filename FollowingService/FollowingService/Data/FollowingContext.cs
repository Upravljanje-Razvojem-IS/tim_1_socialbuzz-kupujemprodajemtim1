using FollowingService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Data
{
    public class FollowingContext : DbContext
    {
        public FollowingContext(DbContextOptions<FollowingContext> options) : base(options)
        {
        }

        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>().ToTable("Follow");
        }
    }
}
