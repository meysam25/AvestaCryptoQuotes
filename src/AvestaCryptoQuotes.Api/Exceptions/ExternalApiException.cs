namespace AvestaCryptoQuotes.Api.Exceptions
{
    public class ExternalApiException : BusinessException
    {
        public ExternalApiException(string message) : base(message) { }
    }
}
