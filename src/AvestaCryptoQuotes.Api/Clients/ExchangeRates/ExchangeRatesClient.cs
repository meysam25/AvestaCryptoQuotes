using AvestaCryptoQuotes.Api.Exceptions;
using Microsoft.Extensions.Options;

namespace AvestaCryptoQuotes.Api.Clients.ExchangeRates
{
    public class ExchangeRatesClient(HttpClient _httpClient, ILogger<ExchangeRatesClient> _logger, IOptions<ExchangeRatesOptions> _option) : IExchangeRatesClient
    {
        static readonly string[] TargetCurrencies = { "EUR", "BRL", "GBP", "AUD" };

        public async Task<Dictionary<string, decimal>> GetUsdRatesAsync(CancellationToken cancellationToken)
        {
            var symbols = string.Join(",", TargetCurrencies);

            HttpResponseMessage? response = null;
            try { response = await _httpClient.GetAsync($"/latest?base=USD&symbols={symbols}&access_key={_option.Value?.ApiKey}", cancellationToken); }
            catch { }

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogWarning("ExchangeRates API failed with status code {StatusCode}",response?.StatusCode);

                throw new ExternalApiException("Failed to retrieve exchange rates.");
            }

            var payload = await response.Content.ReadFromJsonAsync<ExchangeRatesResponse>(cancellationToken);

            if (payload == null || payload.Rates.Count == 0)
                throw new ExternalApiException("Exchange rates response is empty.");

            return payload.Rates;
        }
    }
}
