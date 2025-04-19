using LoanFlow.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanFlow.Infrastructure.Persistence
{
    public sealed class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> opts) : base(opts) { }

        public DbSet<LoanRequest> LoanRequests => Set<LoanRequest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoanRequest>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.CustomerName)
                 .IsRequired();

                e.Property(x => x.Amount)
                 .HasPrecision(18, 2);

                // Store enums as strings for readability
                e.Property(x => x.Status)
                 .HasConversion<string>();

                e.Property(x => x.Type)
                 .HasConversion<string>();
            });
        }
    }
}
