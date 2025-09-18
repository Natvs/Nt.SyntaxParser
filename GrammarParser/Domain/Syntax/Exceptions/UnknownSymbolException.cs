namespace GrammarParser.Domain.Syntax.Exceptions
{
    public class UnknownSymbolException : Exception
    {
        public UnknownSymbolException(string name, int line) : base($"Symbol {name} at line {line} was not declared. Unknown symbol.") { }
    }
}
