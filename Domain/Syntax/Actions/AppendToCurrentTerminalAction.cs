using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that appends the read token to the current terminal in the context
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="symbols">List of all symbols</param>
    /// <param name="context">Context of the automaton</param>
    public class AppendToCurrentTerminalAction(SymbolsList symbols, AutomatonContext context) : Action
    {
        public override void Perform(ParsedToken word)
        {
            context.CurrentTerminal += symbols[word.TokenIndex].Name;
        }
    }
}
