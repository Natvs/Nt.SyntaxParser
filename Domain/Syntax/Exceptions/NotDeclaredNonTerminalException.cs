namespace Nt.Syntax.Exceptions
{

    public class NotDeclaredNonTerminalException(string name, int line) : Exception($"Symbol {name} at line {line} is not declared as non terminal")
    {
    }
}
