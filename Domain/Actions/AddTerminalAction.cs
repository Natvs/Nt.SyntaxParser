using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represent an action that adds a new terminal to the grammar
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="context">Automaton context</param>
    internal class AddTerminalAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Add a parsed token as a new terminal of the grammar
        /// </summary>
        /// <param name="token">Parsed token to add as new terminal</param>
        public void Perform(IAutomatonToken<string> token)
        {
            try
            {
                if (token is AutomatonToken automatonToken)
                {
                    var name = grammar.RemoveEscapeCharacter(automatonToken.Value);
                    grammar.GetBuilder().AddTerminal(name, automatonToken.Line);
                    context.Terminal = grammar.Terminals.Get((t) => t.Name == name);
                }
            }
            catch (RegisteredNonTerminalException) { }
            catch (RegisteredTerminalException) { }
        }
    }
}
