using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    public class AddNewRuleAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        /// <summary>
        /// Adds the symbol of a rule
        /// </summary>
        /// <param name="word"></param>
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                context.Rule = new Rule(grammar);
                context.Rule.GetBuilder().SetToken(new NonTerminal(token.Symbol, token.Line));
                grammar.Rules.Add(context.Rule);
            }
        }
    }
}
