using GrammarParser.Syntax.Exceptions;
using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Structures;

namespace GrammarParser.Syntax.Actions
{
    public class AddSameRuleAction(Grammar grammar) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new NullRuleException($"Attempting to set a new rule with same symbol while the rule does not exists");
            if (rule.Token == null) throw new Exception("Attempting to set a new rule with same symbol while the symbol is not defined");
            return grammar.AddRule(rule.Token.Index, word.Line);
        }
    }
}
