using Microsoft.AspNetCore.Mvc;
using TradingCalculator.Core.Entities;
using TradingCalculator.Application.UseCases;

namespace TradingCalculator.Controllers;

[ApiController]
[Route("api/pnl")]
public class PnlController : ControllerBase
{
    private readonly CalculatePnLUseCase _useCase;

    public PnlController(CalculatePnLUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<ActionResult<PnlResult>> Calculate(PnlRequest req)
    {
        var result = await _useCase.Execute(req);
        return Ok(result);
    }
}