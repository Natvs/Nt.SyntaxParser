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
            var new_token = grammar.ParseToGrammarToken(word);
            rule.AddDerivationToken(new_token);
            return rule;
        }
    }
}
