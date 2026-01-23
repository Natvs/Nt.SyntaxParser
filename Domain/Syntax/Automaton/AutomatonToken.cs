using Nt.Parser.Structures;
using Nt.Automaton.Tokens;
using Nt.Parser.Symbols;

namespace Nt.Syntax.Automaton
{
    public class AutomatonToken : ParsedToken<ISymbol>, IAutomatonToken<string>
    {
        public string Value { get; }

        public AutomatonToken(ParsedToken<ISymbol> token) : base(token.Symbol, token.Line)
        {
            Value = token.Symbol.Name;
        }

        public AutomatonToken(ISymbol symbol, int line) : base(symbol, line)
        {
            Value = symbol.Name;
        }
    }
}
