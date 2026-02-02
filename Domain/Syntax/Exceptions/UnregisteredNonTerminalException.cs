namespace Nt.Syntax.Exceptions
{

    public class UnregisteredNonTerminalException(string name, int line) : InternalException($"Symbol {name} at line {line} is not declared as non terminal")
    {
    }
}
