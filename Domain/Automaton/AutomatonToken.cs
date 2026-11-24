using Nt.Automaton.Tokens;
using Nt.Parser.Symbols;
using Nt.Parser.Structures;

namespace Nt.Syntax.Automaton
{
    internal class AutomatonToken(ParsedToken token) : ParsedToken(token.Symbol, token.Line), IAutomatonToken<string>
    {
        public string Value { get; } = token.Symbol.Name;
    }
}
