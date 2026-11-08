using Nt.Syntax.Exceptions;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Structures;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class SetEscapeCharAction(Grammar grammar) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                if (automatonToken.Symbol.Name.Length != 1)
                {
                    throw new InvalidEscapeCharSymbolException(automatonToken.Symbol.Name, automatonToken.Line);
                }
                grammar.SetEscapeChar(automatonToken.Symbol.Name.ToCharArray()[0]);
            }
        }
    }

}
