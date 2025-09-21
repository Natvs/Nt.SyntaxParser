using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Structures;

namespace Nt.SyntaxParser.Syntax.Actions
{
    public class AddImportPathAction(TokensList tokens, ImportPath path) : IAction
    {
        /// <summary>
        /// Adds the token read to the import path.
        /// </summary>
        /// <param name="word">The token that is read</param>
        public void Perform(ParsedToken word)
        {
            path.Path.Add(tokens[word.TokenIndex].Name);
        }
    }

}
