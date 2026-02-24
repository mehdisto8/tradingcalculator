using TradingCalculator.Core.Entities;

namespace TradingCalculator.Core.Ports;

public interface ISymbolRepository
{
    Task SaveAllAsync(List<Symbol> symbols);
}