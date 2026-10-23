using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentImportFileAction(AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Appends a parsed token to the current import file being built 
        /// </summary>
        /// <param name="token">The token read</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                context.CurrentImportFile += automatonToken.Symbol.Name;
            }
        }
    }

}
