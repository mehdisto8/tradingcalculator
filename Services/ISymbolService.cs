using TradingCalculator.Models;

namespace TradingCalculator.Services;

public interface ISymbolService
{
    Task SaveCurrenciesAsync(CurrencyApiResponse response);
}
