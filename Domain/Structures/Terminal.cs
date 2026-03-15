using Nt.Parser.Structures;
using Nt.Parser.Symbols;

namespace Nt.Syntax.Structures
{
    public class Terminal : GrammarToken
    {
        public Terminal(ISymbol symbol, int line): base(GrammarTokenType.Terminal, symbol, line) { }
    }
}
