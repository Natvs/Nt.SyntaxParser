using Nt.Syntax.Exceptions;
using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddRegExSymbolsAction(Grammar grammar, SymbolsList symbols) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            if (regex == null) throw new NullRegexException("Attempting to add symbols to a non existent regular expression");

            // Handles escape characters
            var new_token = grammar.RemoveEscapeCharacters(symbols[word.TokenIndex].Name);

            // Adds the symbols to the regex
            regex.AddSymbols(new_token);
            return regex;
        }
    }
}
