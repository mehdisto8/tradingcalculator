using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using TradingCalculator.Models;
using TradingCalculator.Core.Ports;
using TradingCalculator.Infrastructure.External;
using TradingCalculator.Infrastructure.Repositories;
using TradingCalculator.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    return ConnectionMultiplexer.Connect(
        "127.0.0.1:6379,abortConnect=false,connectTimeout=5000,syncTimeout=5000"
    );
});

builder.Services.AddHttpClient<IPriceProvider, BinancePriceProvider>();

builder.Services.AddScoped<ISymbolRepository, SymbolRepository>();

builder.Services.AddScoped<ImportSymbolsUseCase>();
builder.Services.AddScoped<CalculatePnLUseCase>();

builder.Services.AddDbContext<TradingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication? app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(
            "{\"error\":\"Something went wrong\"}"
        );
    });
});
app.UseAuthorization();
app.MapControllers();
app.Run();
