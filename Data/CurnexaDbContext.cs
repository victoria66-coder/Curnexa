using Curnexa.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Curnexa.Data
{
    public class CurnexaDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public CurnexaDbContext(DbContextOptions<CurnexaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Currency> Currencies => Set<Currency>();

        public DbSet<Wallet> Wallets => Set<Wallet>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Transaction> Transactions => Set<Transaction>();

        public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();

        public DbSet<Conversion> Conversions => Set<Conversion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>()
                .Property(x => x.Balance)
                .HasPrecision(20, 4);

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Amount)
               .HasPrecision(20, 4);

            modelBuilder.Entity<ExchangeRate>()
                 .Property(x => x.Rate)
                .HasPrecision(20, 4);

            modelBuilder.Entity<Conversion>()
                 .Property(x => x.FromAmount)
                .HasPrecision(20, 4);

            modelBuilder.Entity<Conversion>()
                 .Property(x => x.ToAmount)
                .HasPrecision(20, 4);

            modelBuilder.Entity<Conversion>()
                 .Property(x => x.ExchangeRate)
                .HasPrecision(20, 6);


        }
    }
}
