using AvestaCryptoQuotes.Api.Services;

namespace AvestaCryptoQuotes.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDI(this IServiceCollection services) {
            services.AddScoped<ICryptoQuoteService, CryptoQuoteService>();
            return services;
        }
    }
}
