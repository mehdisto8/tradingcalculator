namespace TradingCalculator1.Models;

public class Symbol
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal Rate { get; set; }
}
