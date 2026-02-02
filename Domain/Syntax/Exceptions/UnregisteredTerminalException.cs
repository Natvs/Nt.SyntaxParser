namespace Nt.Syntax.Exceptions
{
    public class UnregisteredTerminalException(string name, int line) : InternalException($"Symbol {name} at line {line} is not declared as a terminal")
    {
    }
}
