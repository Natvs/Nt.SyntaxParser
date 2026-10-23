using Nt.Syntax.Exceptions;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    public class AddSameRuleAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        
        public void Perform(IAutomatonToken<string> token)
        {
            if (context.Rule == null) throw new NullRuleException($"Attempting to set a new rule with same symbol while the rule does not exists");
            if (context.Rule.Token == null) throw new InternalException("Attempting to set a new rule with same symbol while the symbol is not defined");
            if (token is AutomatonToken automatonToken)
            {
                var rule = new Rule(grammar);
                rule.GetBuilder().SetToken(new NonTerminal(context.Rule.Token.Symbol, automatonToken.Line));
                context.Rule = rule;
                grammar.Rules.Add(context.Rule);
            }
        }
    }
}
