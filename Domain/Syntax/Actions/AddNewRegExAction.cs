using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Structures;

namespace Nt.SyntaxParser.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            return grammar.AddRegularExpression(tokens[word.TokenIndex].Name, word.Line);
        }
    }
}
