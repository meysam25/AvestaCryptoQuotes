using AvestaCryptoQuotes.Api.Contracts;

namespace AvestaCryptoQuotes.Api.Services
{
    public interface ICryptoQuoteService
    {
        Task<CryptoQuoteResponse> GetLatestQuotesAsync(string symbol, CancellationToken cancellationToken);
    }
}
