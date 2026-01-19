using Nt.Parser.Structures;

namespace Nt.Syntax.Structures
{
    public class Terminal : GrammarToken
    {
        public Terminal(Symbol symbol, int line): base(GrammarTokenType.Terminal, symbol, line) { }
    }
}
