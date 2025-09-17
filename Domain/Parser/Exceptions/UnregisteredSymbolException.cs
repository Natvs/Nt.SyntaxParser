namespace GrammarReader.Domain.Parser.Exceptions
{
    public class UnregisteredSymbolException : Exception
    {
        public UnregisteredSymbolException(string symbol) : base($"Symbol {symbol} is not registered") { }
    }
}
