using AvestaCryptoQuotes.Api.Middlewares;

namespace AvestaCryptoQuotes.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddleWares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
