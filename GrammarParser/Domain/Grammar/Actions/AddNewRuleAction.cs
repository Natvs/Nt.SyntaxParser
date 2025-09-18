using GrammarParser.Domain.Grammar.Structures;
using GrammarParser.Domain.Parser.Structures;

namespace GrammarParser.Domain.Grammar.Actions
{
    public class AddNewRuleAction(Structures.Grammar grammar, TokensList tokens) : RuleAction
    {
        /// <summary>
        /// Adds the symbol of a rule
        /// </summary>
        /// <param name="word"></param>
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            return grammar.AddRule(tokens[word.TokenIndex].Name, word.Line);
        }
    }

    public class AddSameRuleAction(Structures.Grammar grammar) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new Exception("Attempting to set a new rule with same symbol while the rule is not defined");
            if (rule.Token == null) throw new Exception("Attempting to set a new rule with same symbol while the symbol is not defined");
            return grammar.AddRule(rule.Token.Index, word.Line);
        }
    }

    public class AddNewRegExAction(Structures.Grammar grammar, TokensList tokens) : RegExAction
    {
        public override RegularExpression? Perform(RegularExpression? regex, ParsedToken word)
        {
            return grammar.AddRegularExpression(tokens[word.TokenIndex].Name, word.Line);
        }
    }
}
