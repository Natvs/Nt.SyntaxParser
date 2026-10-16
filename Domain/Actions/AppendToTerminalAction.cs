using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that appends the read token to the current terminal in the context
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="symbols">List of all symbols</param>
    /// <param name="context">Context of the automaton</param>
    public class AppendToTerminalAction(AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                if (!context.AddingToTerminal)
                {
                    context.Terminal = "";
                    context.AddingToTerminal = true;
                }
                context.Terminal += token.Symbol.Name;
            }
        }
    }
}
