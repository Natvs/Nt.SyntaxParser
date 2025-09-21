using Nt.SyntaxParser.Syntax.Exceptions;
using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Structures;

namespace Nt.SyntaxParser.Syntax.Actions
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
