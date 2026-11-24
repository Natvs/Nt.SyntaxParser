using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{
    internal class ExpectedAction(string expected) : ITokenAction<string>
    {

        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken) 
            {
                throw new ExpectedException(automatonToken.Line, expected, automatonToken.Value);
            }
        }

    }

}
