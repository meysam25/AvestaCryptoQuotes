# Avesta Crypto Quotes API

The application is designed according to the assignment and uses the allowed APIs only.
Depending on the ExchangeRates API subscription plan, USD as base currency may require a paid plan.
If base=USD is not available in the free plan, the same calculation can be adapted by converting from the API default base currency.


ASP.NET Core Web API application created as part of the Avesta Software Engineer recruitment test.

The application accepts a cryptocurrency symbol and returns its latest quote in:

- USD
- EUR
- BRL
- GBP
- AUD

## APIs Used

This project uses only the APIs allowed by the assignment:

- CoinMarketCap API
- ExchangeRates API

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- HttpClientFactory
- Options Pattern
- Dependency Injection
- xUnit
- Moq
- FluentAssertions
- Swagger

## Endpoint
```http
GET /api/crypto-quotes/{symbol}
