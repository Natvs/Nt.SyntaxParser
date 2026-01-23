using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    public class AddNewRuleAction(Grammar grammar, AutomatonContext context) : IAction<string>
    {
        /// <summary>
        /// Adds the symbol of a rule
        /// </summary>
        /// <param name="word"></param>
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                context.Rule = grammar.AddRule(new(token.Symbol, token.Line));
            }
        }
    }
}
