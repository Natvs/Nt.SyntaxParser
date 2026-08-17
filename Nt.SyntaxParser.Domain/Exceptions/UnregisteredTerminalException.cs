namespace Nt.Syntax.Exceptions
{
    public class UnregisteredTerminalException(string name, int line) : InternalException($"Symbol {name} at line {line} is not declared as a terminal")
    {
        public string Name { get; } = name;
        public int Line { get; } = line;
    }
}
