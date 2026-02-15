namespace TradingCalculator1.Models;

public class PnlRequest
{
    public string Symbol { get; set; }
    public decimal EntryPrice { get; set; }
    public decimal Quantity { get; set; }
    public string Side { get; set; }
}
