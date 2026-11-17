using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions
{
    internal class CreateRegexAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                var axiom = grammar.RemoveEscapeCharacter(automatonToken.Value);
                grammar.GetBuilder().AddRegularExpression(axiom, automatonToken.Line, out var regex);
                context.Regex = regex;
            }
        }
    }
}
