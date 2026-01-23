using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that adds new non terminal
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all tokens</param>
    public class AddNonTerminalAction(Structures.Grammar grammar, AutomatonContext context) : IAction<string>
    {
        /// <summary>
        /// Adds a parsed token as new non terminal of the grammar
        /// </summary>
        /// <param name="word">Parsed token to add as new non terminal</param>
        public void Perform(IAutomatonToken<string> word)
        {
            try
            {
                if (!context.CurrentNonTerminal.Equals(""))
                {
                    grammar.AddNonTerminal(grammar.RemoveEscapeCharacter(context.CurrentNonTerminal));
                    context.CurrentNonTerminal = "";
                }
            }
            catch (RegisteredNonTerminalException) { } // Ignore those 2 exceptions for now
            catch (RegisteredTerminalException) { } // Maybe someone should remove them later
        }
    }

}
