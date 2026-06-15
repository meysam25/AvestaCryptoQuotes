namespace AvestaCryptoQuotes.Api.Clients.ExchangeRates
{
    public interface IExchangeRatesClient
    {
        Task<Dictionary<string, decimal>> GetUsdRatesAsync(CancellationToken cancellationToken);
    }
}
