namespace GrammarParser.Domain.Parsing.Exceptions
{
    public class RegisteredSymbolException : Exception
    {
        public RegisteredSymbolException(string symbol) : base($"Symbol {symbol} already registered") { }
    }
}
