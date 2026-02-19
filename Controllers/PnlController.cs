using Microsoft.AspNetCore.Mvc;
using TradingCalculator.Models;
using TradingCalculator.Services;

namespace TradingCalculator.Controllers;

[ApiController]
[Route("api/pnl")]
public class PnlController : ControllerBase
{
    private readonly IPriceService _priceService;

    public PnlController(IPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpPost]
    public async Task<ActionResult<PnlResult>> Calculate(PnlRequest req)
    {
        var current = await _priceService.GetPriceAsync(req.Symbol);

        decimal pnl = req.Side.ToLower() == "long"
            ? (current - req.EntryPrice) * req.Quantity
            : (req.EntryPrice - current) * req.Quantity;

        var cost = req.EntryPrice * req.Quantity;
        var roi = cost == 0 ? 0 : pnl / cost * 100;

        return Ok(new PnlResult
        {
            Pnl = pnl,
            RoiPercent = roi
        });
    }
}
