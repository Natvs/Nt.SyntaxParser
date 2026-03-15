using Nt.Parser.Structures;
using Nt.Parser.Symbols;

namespace Nt.Syntax.Structures
{
    public class NonTerminal:  GrammarToken
    {
        public NonTerminal(ISymbol symbol, int line): base(GrammarTokenType.NonTerminal, symbol, line) { }
    }
}
