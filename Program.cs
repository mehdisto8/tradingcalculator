using StackExchange.Redis;
using TradingCalculator1.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Redis (retry + crash نکن)
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = "localhost:6379,abortConnect=false";
    return ConnectionMultiplexer.Connect(configuration);
});


// Price Service
builder.Services.AddHttpClient<IPriceService, BinancePriceService>();


var app = builder.Build();


// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
