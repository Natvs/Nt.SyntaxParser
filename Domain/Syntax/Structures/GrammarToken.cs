using Nt.Parser.Structures;

namespace Nt.Syntax.Structures
{
    public enum GrammarTokenType
    {
        NonTerminal,
        Terminal
    }
    
    public class GrammarToken(GrammarTokenType type, Symbol symbol, int line)
    {
        public GrammarTokenType Type { get; protected set; } = type;
        public Symbol Symbol { get; } = symbol;

        public string Name { get => Symbol.Name; }
        public int Line { get; } = line;

        public override string ToString() => $"(Name: {Symbol.Name}, Line: {Line})";

    }
}
