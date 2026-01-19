using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    /// <summary>
    /// Represents an action that appends the read token to the current terminal in the context
    /// </summary>
    /// <param name="grammar">Grammar datas</param>
    /// <param name="symbols">List of all symbols</param>
    /// <param name="context">Context of the automaton</param>
    public class AppendToCurrentTerminalAction(AutomatonContext context) : Action
    {
        public override void Perform(ParsedToken word)
        {
            context.CurrentTerminal += word.Symbol.Name;
        }
    }
}
