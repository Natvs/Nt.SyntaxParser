namespace Nt.Syntax.Exceptions
{
    public class UnknownSymbolException(string name, int line) : Exception($"Symbol {name} at line {line} was not declared. Unknown symbol.")
    {
        public string TokenName { get; } = name;
        public int Line { get; } = line;
    }
}
