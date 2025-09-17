using GrammarReader.Code.Parser.Structures;
using GrammarReader.Code.Grammar.Structures;

namespace GrammarReader.Code.Grammar.Actions
{
    public class AddImportPathAction(TokensList tokens, ImportPath path): IAction
    {
        public void Perform(ParsedToken word)
        {
            path.Path.Add(tokens[word.TokenIndex].Name);   
        }
    }

}
