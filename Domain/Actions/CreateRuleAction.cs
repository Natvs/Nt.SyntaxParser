using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    internal class CreateRuleAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {

        /// <summary>
        /// Create a new rule in the grammar based on the token read
        /// </summary>
        /// <param name="token">The token read</param>
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                var axiom = grammar.RemoveEscapeCharacter(automatonToken.Value);
                grammar.GetBuilder().AddRule(axiom, automatonToken.Line, out var rule);
                context.Rule = rule;
            }
        }

    }

}
