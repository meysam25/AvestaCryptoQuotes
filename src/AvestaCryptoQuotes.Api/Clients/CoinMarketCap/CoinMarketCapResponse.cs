namespace AvestaCryptoQuotes.Api.Clients.CoinMarketCap
{
    public class CoinMarketCapResponse
    {
        public Dictionary<string, CoinMarketCapCryptoData> Data { get; set; } = new();
    }

    public class CoinMarketCapCryptoData
    {
        public Dictionary<string, CoinMarketCapQuote> Quote { get; set; } = new();
    }

    public class CoinMarketCapQuote
    {
        public decimal Price { get; set; }
    }
}
