using GrammarParser.Syntax.Exceptions;
using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Structures;

namespace GrammarParser.Syntax.Actions
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
