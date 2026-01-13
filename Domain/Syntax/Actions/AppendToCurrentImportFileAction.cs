using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentImportFileAction(SymbolsList symbols, AutomatonContext context) : Action
    {
        /// <summary>
        /// Appends a parsed token to the current import file being built 
        /// </summary>
        /// <param name="word"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Perform(ParsedToken word)
        {
            context.CurrentImportFile += symbols[word.TokenIndex];
        }
    }

}
