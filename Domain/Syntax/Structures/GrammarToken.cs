using Nt.Parser.Structures;
using Nt.Parser.Symbols;

namespace Nt.Syntax.Structures
{
    public enum GrammarTokenType
    {
        NonTerminal,
        Terminal
    }
    
    public class GrammarToken(GrammarTokenType type, ISymbol symbol, int line)
    {
        public GrammarTokenType Type { get; } = type;
        public ISymbol Symbol { get; } = symbol;

        public string Name { get => Symbol.Name; }
        public int Line { get; } = line;

        public override string ToString() => $"(Name: {Symbol.Name}, Line: {Line})";

    }
}
