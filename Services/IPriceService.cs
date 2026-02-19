namespace TradingCalculator.Services;

public interface IPriceService
{
    Task<decimal> GetPriceAsync(string symbol);
}
