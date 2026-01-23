using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using System.Text.RegularExpressions;

namespace Nt.Syntax.Actions
{
    public class AddRegExSymbolsAction(Grammar grammar, AutomatonContext context) : IAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            if (context.Regex == null) throw new NullRegexException("Attempting to add symbols to a non existent regular expression");

            if (word is AutomatonToken token)
            {
                // Handles escape characters
                var new_token = grammar.RemoveEscapeCharacter(token.Symbol.Name);

                // Adds the symbols to the regex
                context.Regex.AddSymbols(new_token);
            }
        }
    }
}
