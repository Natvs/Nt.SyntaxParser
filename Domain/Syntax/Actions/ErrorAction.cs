using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class ErrorAction() : IAction<string>
    {

        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                throw new SyntaxError(token.Symbol.Name, token.Line);
            }
        }

    }
}
