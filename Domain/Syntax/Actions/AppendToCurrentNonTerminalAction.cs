using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentNonTerminalAction(AutomatonContext context) : Action
    {
        /// <summary>
        /// Appends a parsed token to the current non terminal being built
        /// </summary>
        /// <param name="word">Parsed token to append</param>
        public override void Perform(ParsedToken word)
        {
            context.CurrentNonTerminal += word.Symbol.Name;
        }
    }

}
