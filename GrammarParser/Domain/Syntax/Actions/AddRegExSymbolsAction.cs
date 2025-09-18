using GrammarParser.Domain.Syntax.Structures;
using GrammarParser.Domain.Parsing.Structures;
using GrammarParser.Domain.Syntax.Exceptions;

namespace GrammarParser.Domain.Syntax.Actions
{
    public class AddRegExSymbolsAction(TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            if (regex == null) throw new NullRegexException("Attempting to add symbols to a non existent regular expression");

            string token = tokens[word.TokenIndex].Name;
            if (token.StartsWith('\\')) token = token.Substring(1); // Handles an escape char

            regex.AddSymbols(token);
            return regex;
        }
    }
}
