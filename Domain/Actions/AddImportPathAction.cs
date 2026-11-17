using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    internal class AddImportPathAction(AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Add the token read to the import path.
        /// </summary>
        /// <param name="token">The token that is read</param>
        public void Perform(IAutomatonToken<string> token)
        {
            context.ImportPath.AddPath(token.Value);
        }
    }

}
