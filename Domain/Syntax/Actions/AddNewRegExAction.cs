using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Structures;

namespace GrammarParser.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            return grammar.AddRegularExpression(tokens[word.TokenIndex].Name, word.Line);
        }
    }
}
