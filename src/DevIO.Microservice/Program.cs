var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
var app = builder.Build(); 
app.MapGrpcService<CryptoCurrencyService>(); 

app.Run();
