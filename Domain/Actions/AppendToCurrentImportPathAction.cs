using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentImportPathAction(AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Appends a parsed token to the current import path being built 
        /// </summary>
        /// <param name="token">The token read</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                context.CurrentImportPath += automatonToken.Symbol.Name;
            }
        }

    }

}
