using AvestaCryptoQuotes.Api.Clients.CoinMarketCap;
using AvestaCryptoQuotes.Api.Clients.ExchangeRates;
using AvestaCryptoQuotes.Api.Exceptions;
using AvestaCryptoQuotes.Api.Services;
using FluentAssertions;
using Moq;

namespace AvestaCryptoQuotes.Tests
{
    public class CryptoQuoteServiceTests
    {
        [Fact]
        public async Task GetLatestQuotesAsync_ShouldReturnQuotes_WhenSymbolIsValid()
        {
            var coinClient = new Mock<ICoinMarketCapClient>();
            var ratesClient = new Mock<IExchangeRatesClient>();

            coinClient
                .Setup(x => x.GetUsdPriceAsync("BTC", It.IsAny<CancellationToken>()))
                .ReturnsAsync(100m);

            ratesClient
                .Setup(x => x.GetUsdRatesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Dictionary<string, decimal>
                {
                    ["EUR"] = 0.9m,
                    ["BRL"] = 5m,
                    ["GBP"] = 0.8m,
                    ["AUD"] = 1.5m
                });

            var service = new CryptoQuoteService(coinClient.Object, ratesClient.Object);

            var result = await service.GetLatestQuotesAsync("btc", CancellationToken.None);

            result.Symbol.Should().Be("BTC");
            result.Quotes.Should().ContainKey("USD");
            result.Quotes["USD"].Should().Be(100m);
            result.Quotes["EUR"].Should().Be(90m);
            result.Quotes["BRL"].Should().Be(500m);
            result.Quotes["GBP"].Should().Be(80m);
            result.Quotes["AUD"].Should().Be(150m);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("BTC-USD")]
        [InlineData("THIS_SYMBOL_IS_TOO_LONG")]
        public async Task GetLatestQuotesAsync_ShouldThrowArgumentException_WhenSymbolIsInvalid(string symbol)
        {
            var coinClient = new Mock<ICoinMarketCapClient>();
            var ratesClient = new Mock<IExchangeRatesClient>();
            var service = new CryptoQuoteService(coinClient.Object, ratesClient.Object);

            var act = async () => await service.GetLatestQuotesAsync(symbol, CancellationToken.None);

            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task GetLatestQuotesAsync_ShouldThrow_WhenRequiredRateIsMissing()
        {
            var coinClient = new Mock<ICoinMarketCapClient>();
            var ratesClient = new Mock<IExchangeRatesClient>();

            coinClient
                .Setup(x => x.GetUsdPriceAsync("BTC", It.IsAny<CancellationToken>()))
                .ReturnsAsync(100m);

            ratesClient
                .Setup(x => x.GetUsdRatesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Dictionary<string, decimal> { ["EUR"] = 0.9m });

            var service = new CryptoQuoteService(coinClient.Object, ratesClient.Object);
            var act = async () => await service.GetLatestQuotesAsync("BTC", CancellationToken.None);

            await act.Should().ThrowAsync<Exception>();
        }
    }
}
