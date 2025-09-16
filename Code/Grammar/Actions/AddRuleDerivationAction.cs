using GrammarReader.Code.Grammar.Structures;
using GrammarReader.Code.Parser.Structures;

namespace GrammarReader.Code.Grammar.Actions
{
    public class AddRuleDerivationAction(Structures.Grammar grammar, TokensList tokens) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new Exception("Attempting to write to a derivation of a non existent rule");
            if (grammar.Terminals.Contains(tokens[word.TokenIndex].Name))
            {
                rule.AddTerminal(grammar.GetTerminalIndex(tokens[word.TokenIndex].Name), word.Line);
                return rule;
            }
            else if (grammar.NonTerminals.Contains(tokens[word.TokenIndex].Name))
            {
                rule.AddNonTerminal(grammar.GetNonTerminalIndex(tokens[word.TokenIndex].Name), word.Line);
                return rule;
            }
            throw new Exceptions.NotDeclaredSymbolException(tokens[word.TokenIndex].Name, word.Line);
        }
    }

    public class AddRegExSymbolsAction(Structures.Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            if (regex == null) throw new Exception("Attempting to add symbols to a non existent regular expression");            
            regex.AddSymbols(tokens[word.TokenIndex].Name);
            return regex;
        }
    }
}
