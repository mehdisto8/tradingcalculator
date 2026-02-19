namespace TradingCalculator.Models;

public class CurrencyApiResponse
{
    public bool Success { get; set; }

    public Dictionary<string, CurrencyItem> Data { get; set; } = new();

    public Metadata Metadata { get; set; } = new();
}

public class CurrencyItem
{
    public string Rate { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class Metadata
{
    public int Total_Currencies { get; set; }
    public DateTime Timestamp { get; set; }
}
