using DevIO.Rpc.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Net;
using static DevIO.Rpc.Services.CryptoService;

public class CryptoCurrencyService : CryptoServiceBase
{
    private readonly ILogger<CryptoCurrencyService> _logger;

    public CryptoCurrencyService(ILogger<CryptoCurrencyService> logger)
    {
        _logger = logger;
    }

    public override Task<CryptoResponse> GetCryptoCurrencies(Empty request, ServerCallContext context)
    {
        var ip = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList
            .FirstOrDefault(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        var host = ip is null ? "N/D" : ip.ToString();

        _logger.LogInformation(host); 

        var response = new CryptoResponse
        {
            Host = host
        };

        var cryptos = GetCryptos().ToArray();

        response.CryptoCurrencies.AddRange(cryptos);

        return Task.FromResult(response);
    }

    private IEnumerable<Crypto> GetCryptos()
    {
        yield return new Crypto { Sigla = "SAND", Name = "The Sandbox", Price = "R$ 23,00" };
        yield return new Crypto { Sigla = "AXS", Name = "Axie Infinity ", Price = "R$ 335,00" };
        yield return new Crypto { Sigla = "SOL", Name = "Solana", Price = "R$ 526,00" };
        yield return new Crypto { Sigla = "MANA", Name = "Decentraland ", Price = "R$ 16,73" };
        yield return new Crypto { Sigla = "GALA", Name = "GALA", Price = "R$ 1,90" };
    }
}