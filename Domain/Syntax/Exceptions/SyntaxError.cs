namespace Nt.Syntax.Exceptions
{
    public class SyntaxError(string name, int line) : InternalException($"Syntax error in grammar at line {line}: unknown symbol {name}")
    {
        public string Name { get; } = name;
        public int Line { get; } = line;
    }
}
