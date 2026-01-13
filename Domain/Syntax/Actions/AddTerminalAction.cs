using Nt.Parsing.Structures;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that adds a new terminal to the grammar
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="tokens">List of all symbols</param>
    public class AddTerminalAction(Structures.Grammar grammar, AutomatonContext context) : Action
    {
        /// <summary>
        /// Adds a parsed token as a new non terminal of the grammar
        /// </summary>
        /// <param name="word">Parsed token to add as new terminal</param>
        public override void Perform(ParsedToken word)
        {
            try
            {
                if (!context.CurrentTerminal.Equals(""))
                { 
                    grammar.AddTerminal(grammar.RemoveEscapeCharacters(context.CurrentTerminal)); 
                    context.CurrentTerminal = "";
                }
            }
            catch (RegisteredNonTerminalException) { }
            catch (RegisteredTerminalException) { }
        }
    }
}
