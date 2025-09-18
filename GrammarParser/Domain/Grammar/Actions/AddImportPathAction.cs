using GrammarParser.Domain.Grammar.Structures;
using GrammarParser.Domain.Parser.Structures;

namespace GrammarParser.Domain.Grammar.Actions
{
    public class AddImportPathAction(TokensList tokens, ImportPath path) : IAction
    {
        public void Perform(ParsedToken word)
        {
            path.Path.Add(tokens[word.TokenIndex].Name);
        }
    }

}
