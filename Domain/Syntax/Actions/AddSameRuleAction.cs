using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Parser.Structures;
using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions
{
    public class AddSameRuleAction(Grammar grammar, AutomatonContext context) : IAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            if (context.Rule == null) throw new NullRuleException($"Attempting to set a new rule with same symbol while the rule does not exists");
            if (context.Rule.Token == null) throw new Exception("Attempting to set a new rule with same symbol while the symbol is not defined");
            if (word is AutomatonToken token)
            {
                context.Rule = grammar.AddRule(new(context.Rule.Token.Symbol, token.Line));
            }
        }
    }
}
