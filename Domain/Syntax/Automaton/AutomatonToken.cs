using Nt.Parser.Structures;
using Nt.Automaton.Tokens;

namespace Nt.Syntax.Automaton
{
    public class AutomatonToken : ParsedToken, IAutomatonToken<string>
    {
        public string Value { get; }

        public AutomatonToken(ParsedToken token) : base(token.Symbol, token.Line)
        {
            Value = token.Symbol.Name;
        }

        public AutomatonToken(Symbol symbol, int line) : base(symbol, line)
        {
            Value = symbol.Name;
        }
    }
}
