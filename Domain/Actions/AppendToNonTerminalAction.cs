using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AppendToNonTerminalAction(AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Appends a parsed token to the current non terminal being built
        /// </summary>
        /// <param name="token">Parsed token to append</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                if (!context.AddingToNonTerminal)
                {
                    context.NonTerminal = "";
                    context.AddingToNonTerminal = true;
                }
                context.NonTerminal += automatonToken.Symbol.Name;
            }
        }
    }

}
