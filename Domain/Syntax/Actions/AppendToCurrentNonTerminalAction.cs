using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentNonTerminalAction(AutomatonContext context) : IAction<string>
    {
        /// <summary>
        /// Appends a parsed token to the current non terminal being built
        /// </summary>
        /// <param name="word">Parsed token to append</param>
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                context.CurrentNonTerminal += token.Symbol.Name;
            }
        }
    }

}
