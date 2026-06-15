namespace AvestaCryptoQuotes.Api.Contracts
{
    public class CryptoQuoteResponse
    {
        public string Symbol { get; init; } = string.Empty;

        public Dictionary<string, decimal> Quotes { get; init; } = new();
    }
}
