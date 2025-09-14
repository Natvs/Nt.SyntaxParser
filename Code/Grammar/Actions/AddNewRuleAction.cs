using GrammarReader.Code.Class;

namespace GrammarReader.Code.Grammar.Actions
{
    public class AddNewRuleAction(Class.Grammar grammar, TokensList tokens) : RuleAction
    {
        /// <summary>
        /// Adds the symbol of a rule
        /// </summary>
        /// <param name="word"></param>
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            return grammar.AddRule(tokens[word.Value].Name, word.Line);
        }
    }

    public class AddSameRuleAction(Class.Grammar grammar) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new Exception("Attempting to set a new rule with same symbol while the rule is not defined");
            if (rule.Token == null) throw new Exception("Attempting to set a new rule with same symbol while the symbol is not defined");
            return grammar.AddRule(rule.Token.Index, word.Line);
        }
    }
}
