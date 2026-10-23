using Nt.Automaton.Tokens;
using Nt.Parser.Symbols;
using Nt.Parser.Structures;

namespace Nt.Syntax.Automaton
{
    public class AutomatonToken : ParsedToken, IAutomatonToken<string>
    {
        public string Value { get; }

        public AutomatonToken(ParsedToken token) : base(token.Symbol, token.Line)
        {
            Value = token.Symbol.Name;
        }

        public AutomatonToken(ISymbol symbol, int line) : base(symbol, line)
        {
            Value = symbol.Name;
        }
    }
}
