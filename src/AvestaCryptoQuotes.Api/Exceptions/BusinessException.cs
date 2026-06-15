namespace AvestaCryptoQuotes.Api.Exceptions
{
    public abstract class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}
