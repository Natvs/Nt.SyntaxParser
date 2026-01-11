using Nt.Syntax.Exceptions;
using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddRuleDerivationAction(Grammar grammar, SymbolsList symbols) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new NullRuleException("Attempting to write to a derivation of a non existent rule");
            var token = symbols[word.TokenIndex].Name;

            // Handles escape characters
            string new_token = grammar.RemoveEscapeCharacters(token);

            // Adds the symbol to the rule derivation
            if (grammar.Terminals.Contains(new_token))
            {
                rule.AddTerminal(grammar.GetTerminalIndex(new_token), word.Line);
                return rule;
            }
            else if (grammar.NonTerminals.Contains(new_token))
            {
                rule.AddNonTerminal(grammar.GetNonTerminalIndex(new_token), word.Line);
                return rule;
            }
            throw new UnknownSymbolException(new_token, word.Line);
        }
    }
}
