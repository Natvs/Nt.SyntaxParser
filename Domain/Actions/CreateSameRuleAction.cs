using Nt.Syntax.Exceptions;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    internal class CreateSameRuleAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
      
        public void Perform(IAutomatonToken<string> token)
        {
            if (context.Rule == null) throw new InternalException($"Attempting to set a new rule with same symbol while the rule does not exists");
            if (context.Rule.Axiom == null) throw new InternalException("Attempting to set a new rule with same symbol while the symbol is not defined");
            if (token is AutomatonToken automatonToken)
            {
                grammar.GetBuilder().AddRule(context.Rule.Axiom.Name, automatonToken.Line, out var rule);
                context.Rule = rule;
            }
        }

    }
}
