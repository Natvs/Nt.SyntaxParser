namespace GrammarReader.Domain.Parser.Exceptions
{
    public class RegisteredSymbolException : Exception
    {
        public RegisteredSymbolException(string symbol) : base($"Symbol {symbol} already registered") { }
    }
}
