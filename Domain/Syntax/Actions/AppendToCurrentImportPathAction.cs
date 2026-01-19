using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    public class AppendToCurrentImportPathAction(AutomatonContext context) : Action
    {
        /// <summary>
        /// Appends a parsed token to the current import path being built 
        /// </summary>
        /// <param name="word"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Perform(ParsedToken word)
        {
            context.CurrentImportPath += word.Symbol.Name;
        }

    }

}
