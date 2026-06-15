namespace AvestaCryptoQuotes.Api.Clients.CoinMarketCap
{
    public class CoinMarketCapOptions
    {
        public const string SectionName = "CoinMarketCap";

        public string BaseUrl { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;
    }
}
