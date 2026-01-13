namespace Nt.Syntax.Exceptions
{
    public class SyntaxError(string name, int line) : Exception($"Syntax error in grammar at line {line}: unknown symbol {name}")
    { }
}
