using Nt.Parser.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddNewRegExAction(Grammar grammar) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            return grammar.AddRegex(new(word.Symbol, word.Line));
        }
    }
}
