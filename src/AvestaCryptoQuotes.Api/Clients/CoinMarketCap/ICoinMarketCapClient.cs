namespace AvestaCryptoQuotes.Api.Clients.CoinMarketCap
{
    public interface ICoinMarketCapClient
    {
        Task<decimal> GetUsdPriceAsync(string symbol, CancellationToken cancellationToken);
    }
}
