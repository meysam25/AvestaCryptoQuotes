# Answers to Technical Questions

## 1. How long did you spend on the coding assignment? What would you add if you had more time?

I spent around 4 to 5 hours on the coding assignment.

If I had more time, I would add:

- response caching for exchange rates, because currency exchange rates do not need to be fetched on every request, using radis or add policy for preventing sending same request twice to external api in few secound.
- health checks
- add rate limit by client ip
- prevent answer to other domains
- add seq for tracking what going on

---

## 2. What was the most useful feature added to the latest version of your language of choice?

One of the useful recent features is primary constructors, which reduce boilerplate when dependencies are injected into classes.

Example:
```csharp
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
 ```
 
 ## 3. How would you track down a performance issue in production? Have you ever had to do this?
 
 I would start by checking application metrics such as response time, error rate, CPU usage, memory usage, garbage collection.
 Then I would inspect logs and traces to identify whether the bottleneck is inside the application, a database query, an external API call or infrastructure.
 Useful tools include:
 remote debug
 sql profiler
 using seq for log and reading logs for error and warning 
 
 Yes, I have investigated performance issues before. The most common causes were slow database queries, missing indexes, inefficient loops and latency from external services or external api timeout or cetifcation validation timeout, iis config, sql connection pool, iis connection pool, wrong query split in db context, multiple transaction probelm, heavy select and table lock problem.
 
 
 ## 4. What was the latest technical book you have read or tech conference
 
 Honestly, I haven’t kept up with things lately because of my heavy workload, but I’ve been learning new stuff from AI.
 
 ## 5. What do you think about this technical assessment?
 
I think this is a practical and realistic technical assessment.

It evaluates important backend engineering skills such as:

integrating with third-party APIs
handling configuration securely
writing clean and testable code
validating input
handling errors properly
writing unit tests
The task is small enough to complete in a reasonable amount of time, but still leaves room to demonstrate good engineering practices.


## 6. Please describe yourself using JSON.
```json
{
  "name": "Meysam Ghasemzadeh",
  "role": "Software Engineer",
  "primaryLanguage": "C#",
  "frameworks": [".NET", "ASP.NET Core"],
  "skills": [
	"REST APIs",
	"Clean Code",
	"Unit Testing",
	"Dependency Injection",
	"SQL",
	"Git",
	"Reporting",
	"SignalR",
	"..."
  ],
  "interests": [
	"backend development",
	"software architecture",
	"performance optimization",
	"front development",
  ]
}
```
