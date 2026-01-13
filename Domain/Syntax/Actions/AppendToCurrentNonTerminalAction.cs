using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentNonTerminalAction(SymbolsList symbols, AutomatonContext context) : Action
    {
        /// <summary>
        /// Appends a parsed token to the current non terminal being built
        /// </summary>
        /// <param name="word">Parsed token to append</param>
        public override void Perform(ParsedToken word)
        {
            context.CurrentNonTerminal += symbols[word.TokenIndex];
        }
    }

}
