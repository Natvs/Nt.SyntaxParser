using Nt.Syntax.Exceptions;
using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddRegExSymbolsAction(TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            if (regex == null) throw new NullRegexException("Attempting to add symbols to a non existent regular expression");

            string token = tokens[word.TokenIndex].Name;
            regex.AddSymbols(token);
            return regex;
        }
    }
}
