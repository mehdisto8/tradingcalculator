using TradingCalculator.Core.Entities;
using TradingCalculator.Core.Ports;

namespace TradingCalculator.Application.UseCases;

public class CalculatePnLUseCase
{
    private readonly IPriceProvider _priceProvider;

    public CalculatePnLUseCase(IPriceProvider priceProvider)
    {
        _priceProvider = priceProvider;
    }

    public async Task<PnlResult> Execute(PnlRequest req)
    {
        var current = await _priceProvider.GetPriceAsync(req.Symbol);

        decimal pnl = req.Side.ToLower() == "long"
            ? (current - req.EntryPrice) * req.Quantity
            : (req.EntryPrice - current) * req.Quantity;

        var cost = req.EntryPrice * req.Quantity;
        var roi = cost == 0 ? 0 : pnl / cost * 100;

        return new PnlResult
        {
            Pnl = pnl,
            RoiPercent = roi
        };
    }
}