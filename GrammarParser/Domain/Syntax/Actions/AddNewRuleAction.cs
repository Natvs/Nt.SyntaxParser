using GrammarParser.Domain.Syntax.Structures;
using GrammarParser.Domain.Parsing.Structures;
using GrammarParser.Domain.Syntax.Exceptions;

namespace GrammarParser.Domain.Syntax.Actions
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
