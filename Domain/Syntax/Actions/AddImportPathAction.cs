using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Structures;

namespace GrammarParser.Syntax.Actions
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
