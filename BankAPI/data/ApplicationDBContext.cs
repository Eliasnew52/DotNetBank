using BankApi.Models; // Mys API models
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique index for IdentificationNumber in Client
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.IdentificationNumber)
                .IsUnique();

            // Unique index for AccountNumber in Account
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();
        }
    }
}