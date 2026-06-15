using AvestaCryptoQuotes.Api.Exceptions;
using Microsoft.Extensions.Options;

namespace AvestaCryptoQuotes.Api.Clients.CoinMarketCap
{
    public class CoinMarketCapClient(HttpClient _httpClient, IOptions<CoinMarketCapOptions> _options, ILogger<CoinMarketCapClient> _logger) : ICoinMarketCapClient
    {
        public async Task<decimal> GetUsdPriceAsync(string symbol, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/cryptocurrency/quotes/latest?symbol={symbol}&convert=USD");

            request.Headers.Add("X-CMC_PRO_API_KEY", _options.Value?.ApiKey);
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage? response = null; 
            try
            {
                response = await _httpClient.SendAsync(request, cancellationToken);
            }
            catch { }

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogWarning("CoinMarketCap API failed with status code {StatusCode}", response?.StatusCode);

                throw new ExternalApiException("Failed to retrieve data from CoinMarketCap.");
            }

            var payload = await response.Content.ReadFromJsonAsync<CoinMarketCapResponse>(cancellationToken);

            if (payload is null || !payload.Data.TryGetValue(symbol, out var cryptoData) || !cryptoData.Quote.TryGetValue("USD", out var usdQuote))
                throw new NotFoundException($"Cryptocurrency symbol '{symbol}' was not found.");

            return usdQuote.Price;
        }
    }
}
