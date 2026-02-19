namespace TradingCalculator.Models;

public class PnlRequest
{
    public string Symbol { get; set; } = string.Empty;
    public decimal EntryPrice { get; set; }
    public decimal Quantity { get; set; }
    public string Side { get; set; } = string.Empty;
}
