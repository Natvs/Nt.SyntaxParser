using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Structures;

namespace Nt.SyntaxParser.Syntax.Actions
{
    public class AddNewRuleAction(Grammar grammar, TokensList tokens) : RuleAction
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
}
