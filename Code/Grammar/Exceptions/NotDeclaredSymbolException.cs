using GrammarReader.Code.Class;

namespace GrammarReader.Code.Grammar.Exceptions
{
    public class NotDeclaredSymbolException : Exception
    {
        public NotDeclaredSymbolException(string name, int line) : base($"Symbol {name} at line {line} was not declared. Unknown symbol.") { }
    }
}
