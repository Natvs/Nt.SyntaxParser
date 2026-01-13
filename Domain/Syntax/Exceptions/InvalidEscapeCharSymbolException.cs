namespace Nt.Syntax.Exceptions
{
    public class InvalidEscapeCharSymbolException(string name, int line): Exception($"Error at line {line}: invalid escape character {name}")
    { }
}
