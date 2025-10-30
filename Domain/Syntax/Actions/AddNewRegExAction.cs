using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            return grammar.AddRegularExpression(tokens[word.TokenIndex].Name, word.Line);
        }
    }
}
