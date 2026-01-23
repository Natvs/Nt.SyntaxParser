using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AddImportPathAction(AutomatonContext context) : IAction<string>
    {
        /// <summary>
        /// Adds the token read to the import path.
        /// </summary>
        /// <param name="word">The token that is read</param>
        public void Perform(IAutomatonToken<string> word)
        {
            context.ImportPath.AddPath(context.CurrentImportPath);
            context.CurrentImportPath = "";
        }
    }

}
