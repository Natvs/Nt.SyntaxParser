using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that adds a new terminal to the grammar
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    public class AddTerminalAction(Structures.Grammar grammar, AutomatonContext context) : IAction<string>
    {
        /// <summary>
        /// Adds a parsed token as a new non terminal of the grammar
        /// </summary>
        /// <param name="word">Parsed token to add as new terminal</param>
        public void Perform(IAutomatonToken<string> word)
        {
            try
            {
                if (!context.CurrentTerminal.Equals(""))
                { 
                    grammar.AddTerminal(grammar.RemoveEscapeCharacter(context.CurrentTerminal)); 
                }
            }
            catch (RegisteredNonTerminalException) { }
            catch (RegisteredTerminalException) { }

            context.CurrentTerminal = "";
        }
    }
}
