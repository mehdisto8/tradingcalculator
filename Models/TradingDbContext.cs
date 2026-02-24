using Microsoft.EntityFrameworkCore;
using TradingCalculator.Core.Entities;

namespace TradingCalculator.Models;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Symbol> Symbols => Set<Symbol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Symbol>()
            .Property(s => s.Rate)
            .HasPrecision(18, 6);

        modelBuilder.Entity<Symbol>()
            .HasIndex(s => s.Code)
            .IsUnique();
    }
}
