using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class SetEscapeCharAction(Grammar grammar) : IAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                if (token.Symbol.Name.Length != 1)
                {
                    throw new InvalidEscapeCharSymbolException(token.Symbol.Name, token.Line);
                }
                grammar.SetEscapeCharacter(token.Symbol.Name.ToCharArray()[0]);
            }
        }
    }

}
