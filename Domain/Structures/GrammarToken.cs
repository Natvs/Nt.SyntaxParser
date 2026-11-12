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
        public ISymbol Symbol { get; internal set; } = symbol;
        internal IData? Data { get; set; } = null; 

        public string Name { get => Symbol.Name; }
        public int Line { get; } = line;

        public override string ToString() => Symbol.Name;

    }
}
