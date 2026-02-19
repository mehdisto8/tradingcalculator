using Microsoft.AspNetCore.Mvc;
using TradingCalculator1.Models;
using TradingCalculator1.Services;

namespace TradingCalculator1.Controllers;

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
