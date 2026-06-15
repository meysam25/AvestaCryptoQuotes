namespace AvestaCryptoQuotes.Api.Clients.ExchangeRates
{
    public class ExchangeRatesResponse
    {
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}
