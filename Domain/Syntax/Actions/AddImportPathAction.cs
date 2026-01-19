using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    public class AddImportPathAction(AutomatonContext context) : Action
    {
        /// <summary>
        /// Adds the token read to the import path.
        /// </summary>
        /// <param name="word">The token that is read</param>
        public override void Perform(ParsedToken word)
        {
            context.ImportPath.Path.Add(context.CurrentImportPath);
            context.CurrentImportPath = "";
        }
    }

}
