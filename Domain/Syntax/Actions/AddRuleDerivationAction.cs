using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    public class AddRuleDerivationAction(Grammar grammar) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new NullRuleException("Attempting to write to a derivation of a non existent rule");

            // Handles escape characters
            string new_token = grammar.RemoveEscapeCharacters(word.Symbol.Name);

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
