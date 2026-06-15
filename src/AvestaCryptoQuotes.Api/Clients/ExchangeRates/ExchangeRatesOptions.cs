namespace AvestaCryptoQuotes.Api.Clients.ExchangeRates
{
    public class ExchangeRatesOptions
    {
        public const string SectionName = "ExchangeRates";

        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
