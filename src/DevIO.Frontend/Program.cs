using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Balancer;
using Grpc.Net.Client.Configuration;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddSingleton(services =>
{
    var configuration = services.GetService<IConfiguration>();
    var addressService = configuration.GetValue<string>("MICROSERVICE_URL");

    var channel = GrpcChannel.ForAddress(addressService, new GrpcChannelOptions
    {
        Credentials = ChannelCredentials.Insecure,
        ServiceProvider = services,
        ServiceConfig = new ServiceConfig
        {
            LoadBalancingConfigs = { new RoundRobinConfig()}
        }
    });

    return channel;
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();