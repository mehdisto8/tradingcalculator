namespace TradingCalculator1.Services;

public interface IPriceService
{
    Task<decimal> GetPriceAsync(string symbol);
}
