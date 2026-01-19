using Nt.Parser.Structures;

namespace Nt.Syntax.Structures
{
    public class NonTerminal:  GrammarToken
    {
        public NonTerminal(Symbol symbol, int line): base(GrammarTokenType.NonTerminal, symbol, line) { }
    }
}
