using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentImportFileAction(AutomatonContext context) : IAction<string>
    {
        /// <summary>
        /// Appends a parsed token to the current import file being built 
        /// </summary>
        /// <param name="word"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                context.CurrentImportFile += token.Symbol.Name;
            }
        }
    }

}
