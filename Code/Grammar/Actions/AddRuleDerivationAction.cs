using GrammarReader.Code.Grammar.Structures;
using GrammarReader.Code.Parser.Structures;

namespace GrammarReader.Code.Grammar.Actions
{
    public class AddRuleDerivationAction(Structures.Grammar grammar, TokensList tokens) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new Exception("Attempting to write to a derivation of a non existent rule");

            var token = tokens[word.TokenIndex].Name;
            if (token.StartsWith('\\')) token = token.Substring(1); // Handles an escape char

            if (grammar.Terminals.Contains(token))
            {
                rule.AddTerminal(grammar.GetTerminalIndex(token), word.Line);
                return rule;
            }
            else if (grammar.NonTerminals.Contains(token))
            {
                rule.AddNonTerminal(grammar.GetNonTerminalIndex(token), word.Line);
                return rule;
            }
            throw new Exceptions.NotDeclaredSymbolException(token, word.Line);
        }
    }

    public class AddRegExSymbolsAction(Structures.Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {

            var token = tokens[word.TokenIndex].Name;
            if (token.StartsWith('\\')) token = token.Substring(1); // Handles an escape char

            if (regex == null) throw new Exception("Attempting to add symbols to a non existent regular expression");            
            regex.AddSymbols(token);
            return regex;
        }
    }
}
