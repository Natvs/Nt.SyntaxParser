namespace Nt.Syntax.Exceptions
{
    public class AxiomException(string name, int line) : Exception($"The axiom {name} at line {line} is not registered as a non terminal.")
    {
        public string Name { get; } = name;
        public int Line { get; } = line;
    }
}
