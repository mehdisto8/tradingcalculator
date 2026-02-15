using StackExchange.Redis;
using System.Text.Json;

namespace TradingCalculator1.Services;

public class BinancePriceService : IPriceService
{
    private readonly HttpClient _http;
    private readonly IConnectionMultiplexer _mux;

    public BinancePriceService(HttpClient http, IConnectionMultiplexer mux)
    {
        _http = http;
        _mux = mux;
    }

    public async Task<decimal> GetPriceAsync(string symbol)
    {
        var key = $"price:{symbol}";
        IDatabase? redis = null;

        try
        {
            redis = _mux.GetDatabase();

            var cached = await redis.StringGetAsync(key);
            if (cached.HasValue)
                return decimal.Parse(cached!);
        }
        catch
        {
            // اگر ردیس مشکل داشت → مهم نیست
        }

        // برو Binance
        var url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}";
        var json = await _http.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);

        var priceString = doc.RootElement
            .GetProperty("price")
            .GetString();

        if (string.IsNullOrEmpty(priceString))
            throw new Exception("Price not found");

        try
        {
            if (redis != null)
                await redis.StringSetAsync(key, priceString, TimeSpan.FromSeconds(5));
        }
        catch
        {
            // ذخیره نشد هم مهم نیست
        }

        return decimal.Parse(priceString);
    }
}
