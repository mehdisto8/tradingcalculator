using TradingCalculator.Core.Ports;
using StackExchange.Redis;
using System.Text.Json;

namespace TradingCalculator.Infrastructure.External;

public class BinancePriceProvider : IPriceProvider
{
    private readonly HttpClient _http;
    private readonly IConnectionMultiplexer _mux;

    public BinancePriceProvider(HttpClient http, IConnectionMultiplexer mux)
    {
        _http = http;
        _mux = mux;
    }

    public async Task<decimal> GetPriceAsync(string symbol)
    {
        var key = $"price:{symbol}";
        var redis = _mux.GetDatabase();

        try
        {
            var cached = await redis.StringGetAsync(key);
            if (cached.HasValue)
                return decimal.Parse(cached!);
        }
        catch
        {

        }

        var url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}";

        try
        {
            var json = await _http.GetStringAsync(url);

            using var doc = JsonDocument.Parse(json);

            var price = decimal.Parse(
                doc.RootElement.GetProperty("price").GetString()!
            );

            await redis.StringSetAsync(key, price.ToString(), TimeSpan.FromMinutes(1));

            return price;
        }
        catch
        {
            throw new Exception("Invalid symbol or Binance unavailable");
        }
    }
}