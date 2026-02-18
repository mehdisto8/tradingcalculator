using TradingCalculator1.Models;

namespace TradingCalculator1.Services;

public interface ISymbolService
{
    Task SaveCurrenciesAsync(CurrencyApiResponse response);
}
