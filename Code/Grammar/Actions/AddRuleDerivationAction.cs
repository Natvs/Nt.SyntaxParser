using GrammarReader.Code.Class;

namespace GrammarReader.Code.Grammar.Actions
{
    public class AddRuleDerivationAction(Class.Grammar grammar, TokensList tokens) : RuleAction
    {
        public override Rule Perform(Rule rule, ParsedToken word)
        {
            if (rule == null) throw new Exception("Attempting to write to a derivation of a non existent rule");
            if (grammar.Terminals.Contains(tokens[word.Value].Name))
            {
                rule.AddTerminal(grammar.GetTerminalIndex(tokens[word.Value].Name), word.Line);
                return rule;
            }
            else if (grammar.NonTerminals.Contains(tokens[word.Value].Name))
            {
                rule.AddNonTerminal(grammar.GetNonTerminalIndex(tokens[word.Value].Name), word.Line);
                return rule;
            }
            throw new Exceptions.NotDeclaredSymbolException(tokens[word.Value].Name, word.Line);
        }
    }
}
