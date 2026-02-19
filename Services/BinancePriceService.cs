using StackExchange.Redis;
using System.Text.Json;

namespace TradingCalculator1.Services;

public class BinancePriceService : IPriceService
{
    private readonly HttpClient _http;
    private readonly IConnectionMultiplexer _redis;

    public BinancePriceService(HttpClient http, IConnectionMultiplexer redis)
    {
        _http = http;
        _redis = redis;
    }

    public async Task<decimal> GetPriceAsync(string symbol)
    {
        var db = _redis.GetDatabase();
        var cacheKey = $"price:{symbol}";

        var cached = await db.StringGetAsync(cacheKey);
        if (cached.HasValue)
            return decimal.Parse(cached!);

        var url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}";
        var json = await _http.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);
        var priceString = doc.RootElement.GetProperty("price").GetString();

        if (string.IsNullOrEmpty(priceString))
            throw new Exception("Price not found");

        await db.StringSetAsync(cacheKey, priceString, TimeSpan.FromSeconds(5));

        return decimal.Parse(priceString);
    }
}
