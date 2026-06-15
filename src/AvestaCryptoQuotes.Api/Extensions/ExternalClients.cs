using AvestaCryptoQuotes.Api.Clients.CoinMarketCap;
using AvestaCryptoQuotes.Api.Clients.ExchangeRates;

namespace AvestaCryptoQuotes.Api.Extensions
{
    public static class ExternalClients
    {
        static readonly byte Timeoute = 15;
        public static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CoinMarketCapOptions>(configuration.GetSection(CoinMarketCapOptions.SectionName));
            services.Configure<ExchangeRatesOptions>(configuration.GetSection(ExchangeRatesOptions.SectionName));

            var coinMarketCapOptions = configuration.GetOptions<CoinMarketCapOptions>(CoinMarketCapOptions.SectionName);
            var exchangeRatesOptions = configuration.GetOptions< ExchangeRatesOptions>(ExchangeRatesOptions.SectionName);

            services.AddHttpClient<ICoinMarketCapClient, CoinMarketCapClient>(client =>
            {
                client.BaseAddress = new Uri(coinMarketCapOptions?.BaseUrl ?? throw new InvalidOperationException("CoinMarketCap BaseUrl is missing."));
                client.Timeout = TimeSpan.FromSeconds(Timeoute);
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", coinMarketCapOptions?.ApiKey ?? throw new InvalidOperationException("CoinMarketCap ApiKey is missing."));

            });

            services.AddHttpClient<IExchangeRatesClient, ExchangeRatesClient>(client =>
            {
                client.BaseAddress = new Uri(exchangeRatesOptions?.BaseUrl ?? throw new InvalidOperationException("ExchangeRates BaseUrl is missing."));
                client.Timeout = TimeSpan.FromSeconds(Timeoute);
                _ = coinMarketCapOptions?.ApiKey ?? throw new InvalidOperationException("ExchangeRates ApiKey is missing.");
            });

            return services;
        }
    }
}
