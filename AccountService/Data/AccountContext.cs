using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Data
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Conversion> Conversions { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversion>()
                .HasOne(conversion => conversion.CurrencyIn)
                .WithMany()
                .HasForeignKey(conversion => conversion.CurrencyInId);

            modelBuilder.Entity<Conversion>()
                .HasOne(conversion => conversion.CurrencyIn)
                .WithMany()
                .HasForeignKey(conversion => conversion.CurrencyOutId);
        }
    }
}
