using TradingCalculator1.Models;

namespace TradingCalculator1.Services;

public class SymbolService : ISymbolService
{
    private readonly TradingDbContext _context;

    public SymbolService(TradingDbContext context)
    {
        _context = context;
    }

    public async Task SaveCurrenciesAsync(CurrencyApiResponse response)
    {
        foreach (var item in response.Data.Values)
        {
            if (!_context.Symbols.Any(s => s.Code == item.Code))
            {
                _context.Symbols.Add(new Symbol
                {
                    Code = item.Code,
                    Name = item.Name,
                    Rate = decimal.Parse(item.Rate)
                });
            }
        }

        await _context.SaveChangesAsync();
    }
}
