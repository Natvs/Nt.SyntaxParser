using Nt.Parser.Symbols;

namespace Nt.Syntax.Structures
{
    public class Terminal(ISymbol symbol, int line) : GrammarToken(GrammarTokenType.Terminal, symbol, line)
    {
        public override Terminal Clone(int line) => new(Symbol, line) { Data = Data };
    }
}
