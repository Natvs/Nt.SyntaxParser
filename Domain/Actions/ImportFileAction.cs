using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Reads the performed token as a file name and gets its content
    /// </summary>
    internal class ImportFileAction(AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            context.Files.Add(token.Value);
        }
    }

}
