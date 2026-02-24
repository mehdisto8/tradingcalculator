namespace TradingCalculator.Models;

public class CurrencyApiResponse
{
    public bool success { get; set; }

    public Dictionary<string, CurrencyItem> data { get; set; }
}

public class CurrencyItem
{
    public string rate { get; set; }
    public string name { get; set; }
    public string code { get; set; }
}