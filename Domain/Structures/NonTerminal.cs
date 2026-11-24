using Nt.Parser.Symbols;

namespace Nt.Syntax.Structures
{
    public class NonTerminal(ISymbol symbol, int line) :  GrammarToken(GrammarTokenType.NonTerminal, symbol, line)
    {
        public bool IsEmptyGenerator { get; internal set; } = false;
        public override NonTerminal Clone(int line) => new(Symbol, line) { Data = Data };
    }
}
