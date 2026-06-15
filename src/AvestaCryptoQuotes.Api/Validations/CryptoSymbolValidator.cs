using System.Text.RegularExpressions;

namespace AvestaCryptoQuotes.Api.Validations
{
    public static class CryptoSymbolValidator
    {
        static readonly Regex SymbolRegex = new("^[A-Za-z0-9]{2,10}$", RegexOptions.Compiled);

        public static bool IsValid(string? symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) return false;

            return SymbolRegex.IsMatch(symbol);
        }

        public static string Normalize(string symbol) => symbol.Trim().ToUpperInvariant();
    }
}
