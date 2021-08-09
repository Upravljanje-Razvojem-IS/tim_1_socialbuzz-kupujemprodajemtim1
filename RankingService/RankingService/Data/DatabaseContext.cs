using Microsoft.EntityFrameworkCore;
using RankingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankingService.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<RateType> RateTypes { get; set; }
        public virtual DbSet<UserRate> UserRate { get; set; }
        public virtual DbSet<TransportRate> TransportRate { get; set; }
        public virtual DbSet<PostRate> PostRate { get; set; }

    }
}
