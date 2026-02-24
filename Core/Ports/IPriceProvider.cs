namespace TradingCalculator.Core.Ports;

public interface IPriceProvider
{
    Task<decimal> GetPriceAsync(string symbol);
}