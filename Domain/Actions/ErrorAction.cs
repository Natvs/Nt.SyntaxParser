using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class ErrorAction() : ITokenAction<string>
    {

        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                throw new SyntaxError(automatonToken.Symbol.Name, automatonToken.Line);
            }
        }

    }
}
