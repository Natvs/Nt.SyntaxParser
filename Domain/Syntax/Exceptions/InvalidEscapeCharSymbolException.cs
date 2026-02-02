namespace Nt.Syntax.Exceptions
{
    public class InvalidEscapeCharSymbolException(string name, int line): InternalException($"Error at line {line}: invalid escape character {name}")
    { }
}
