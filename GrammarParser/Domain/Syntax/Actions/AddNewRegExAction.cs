using GrammarParser.Domain.Syntax.Structures;
using GrammarParser.Domain.Parsing.Structures;

namespace GrammarParser.Domain.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            return grammar.AddRegularExpression(tokens[word.TokenIndex].Name, word.Line);
        }
    }
}
