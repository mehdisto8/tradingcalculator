using Microsoft.EntityFrameworkCore;
using TradingCalculator.Core.Entities;
using TradingCalculator.Core.Ports;
using TradingCalculator.Models;

namespace TradingCalculator.Infrastructure.Repositories;

public class SymbolRepository : ISymbolRepository
{
    private readonly TradingDbContext _db;

    public SymbolRepository(TradingDbContext db)
    {
        _db = db;
    }

    public async Task SaveAllAsync(List<Symbol> symbols)
    {
        foreach (var symbol in symbols)
        {
            var existing = await _db.Symbols
                .FirstOrDefaultAsync(x => x.Code == symbol.Code);

            if (existing == null)
            {
                // INSERT
                _db.Symbols.Add(symbol);
            }
            else
            {
                // UPDATE
                existing.Name = symbol.Name;
                existing.Rate = symbol.Rate;
            }
        }

        await _db.SaveChangesAsync();
    }
}