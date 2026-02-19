using Microsoft.AspNetCore.Mvc;
using TradingCalculator.Models;
using TradingCalculator.Services;

namespace TradingCalculator.Controllers;

[ApiController]
[Route("api/symbols")]
public class SymbolsController : ControllerBase
{
    private readonly ISymbolService _symbolService;

    public SymbolsController(ISymbolService symbolService)
    {
        _symbolService = symbolService;
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(CurrencyApiResponse response)
    {
        await _symbolService.SaveCurrenciesAsync(response);
        return Ok("Currencies saved successfully.");
    }
}
