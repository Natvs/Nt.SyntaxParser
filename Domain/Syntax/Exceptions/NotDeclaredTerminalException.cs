namespace Nt.Syntax.Exceptions
{
    public class NotDeclaredTerminalException(string name, int line) : Exception($"Symbol {name} at line {line} is not declared as a terminal")
    {
    }
}
