using Microsoft.AspNetCore.Mvc;
using TradingCalculator.Application.UseCases;
using TradingCalculator.Core.Entities;
using TradingCalculator.Models;

namespace TradingCalculator.Controllers;

[ApiController]
[Route("api/symbols")]
public class SymbolsController : ControllerBase
{
    private readonly ImportSymbolsUseCase _useCase;

    public SymbolsController(ImportSymbolsUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(CurrencyApiResponse response)
    {
        await _useCase.Execute(response);
        return Ok("Currencies saved successfully.");
    }
}