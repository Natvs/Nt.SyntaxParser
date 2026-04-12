namespace Nt.Syntax.Exceptions
{
    public class UnknownSymbolException(string name, int line) : InternalException($"Symbol {name} at line {line} was not declared. Unknown symbol.")
    {
        public string Name { get; } = name;
        public int Line { get; } = line;
    }
}
