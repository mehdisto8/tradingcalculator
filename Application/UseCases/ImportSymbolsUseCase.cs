using TradingCalculator.Core.Entities;
using TradingCalculator.Core.Ports;
using TradingCalculator.Models;

namespace TradingCalculator.Application.UseCases;

public class ImportSymbolsUseCase
{
    private readonly ISymbolRepository _repo;

    public ImportSymbolsUseCase(ISymbolRepository repo)
    {
        _repo = repo;
    }

    public async Task Execute(CurrencyApiResponse response)
    {
        var list = response.data
            .Select(x => new Symbol
            {
                Code = x.Value.code,
                Name = x.Value.name,
                Rate = decimal.Parse(x.Value.rate)
            })
            .ToList();

        await _repo.SaveAllAsync(list);
    }
}