using GrammarReader.Domain.Grammar.Structures;
using GrammarReader.Domain.Parser.Structures;

namespace GrammarReader.Domain.Grammar.Actions
{
    public class AddImportPathAction(TokensList tokens, ImportPath path) : IAction
    {
        public void Perform(ParsedToken word)
        {
            path.Path.Add(tokens[word.TokenIndex].Name);
        }
    }

}
