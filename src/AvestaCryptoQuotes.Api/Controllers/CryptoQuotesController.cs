namespace AvestaCryptoQuotes.Api.Controllers
{
    using AvestaCryptoQuotes.Api.Contracts;
    using AvestaCryptoQuotes.Api.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController, Route("api/crypto-quotes")]
    public class CryptoQuotesController(ICryptoQuoteService _cryptoQuoteService)
    {
        [HttpGet("{symbol}")]
        public async Task<CryptoQuoteResponse> GetLatestQuotes(string symbol, CancellationToken cancellationToken) => 
             await _cryptoQuoteService.GetLatestQuotesAsync(symbol, cancellationToken);
    }
}
