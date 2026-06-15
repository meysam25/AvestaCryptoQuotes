using AvestaCryptoQuotes.Api.Clients.CoinMarketCap;
using AvestaCryptoQuotes.Api.Clients.ExchangeRates;
using AvestaCryptoQuotes.Api.Contracts;
using AvestaCryptoQuotes.Api.Exceptions;
using AvestaCryptoQuotes.Api.Validations;

namespace AvestaCryptoQuotes.Api.Services
{

    public class CryptoQuoteService(ICoinMarketCapClient _coinMarketCapClient, IExchangeRatesClient _exchangeRatesClient) : ICryptoQuoteService
    {

        static readonly string[] RequiredCurrencies = { "USD", "EUR", "BRL", "GBP", "AUD" };
        static readonly byte RoundPrecision = 8;

        public async Task<CryptoQuoteResponse> GetLatestQuotesAsync(string symbol, CancellationToken cancellationToken)
        {
            if (!CryptoSymbolValidator.IsValid(symbol))
                throw new ValidationException("Invalid cryptocurrency symbol.");

            var normalizedSymbol = CryptoSymbolValidator.Normalize(symbol);
            var usdPriceTask = _coinMarketCapClient.GetUsdPriceAsync(normalizedSymbol, cancellationToken);
            var ratesTask = _exchangeRatesClient.GetUsdRatesAsync(cancellationToken);

            await Task.WhenAll(usdPriceTask, ratesTask);

            var usdPrice = await usdPriceTask;
            var rates = await ratesTask;

            EnsureRequiredRatesExist(rates);

            return new CryptoQuoteResponse
            {
                Symbol = normalizedSymbol,
                Quotes = new Dictionary<string, decimal>
                {
                    ["USD"] = decimal.Round(usdPrice, RoundPrecision),
                    ["EUR"] = decimal.Round(usdPrice * rates["EUR"], RoundPrecision),
                    ["BRL"] = decimal.Round(usdPrice * rates["BRL"], RoundPrecision),
                    ["GBP"] = decimal.Round(usdPrice * rates["GBP"], RoundPrecision),
                    ["AUD"] = decimal.Round(usdPrice * rates["AUD"], RoundPrecision)
                }
            };
        }

        static void EnsureRequiredRatesExist(Dictionary<string, decimal> rates) {
            foreach (var currency in RequiredCurrencies.Where(c => c != "USD"))
                if (!rates.ContainsKey(currency))
                    throw new ExternalApiException($"Required exchange rate '{currency}' was not returned.");
        }
    }
}
