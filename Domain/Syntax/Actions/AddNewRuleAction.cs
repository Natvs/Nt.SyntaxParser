using Nt.Parser.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddNewRuleAction(Grammar grammar) : RuleAction
    {
        /// <summary>
        /// Adds the symbol of a rule
        /// </summary>
        /// <param name="word"></param>
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            return grammar.AddRule(word.Symbol.Name, word.Line);
        }
    }
}
