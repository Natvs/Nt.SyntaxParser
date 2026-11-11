namespace Nt.Syntax.Exceptions
{
    internal class AxiomException(string name, int line) : InternalException($"The axiom {name} at line {line} is registered as a terminal, but is expected to be a nonterminal.")
    {
        public string Name { get; } = name;
        public int Line { get; } = line;
    }
}
