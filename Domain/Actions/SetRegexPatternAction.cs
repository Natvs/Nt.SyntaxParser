using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Syntax.Actions
{
    internal class SetRegexPatternAction(Grammar grammar, AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            if (token is AutomatonToken automatonToken)
            {
                if (context.Regex == null) throw new InternalException("Attempting to write to a derivation of a non existent rule");

                var new_token = grammar.RemoveEscapeCharacter(automatonToken.Value);
                context.Regex.GetBuilder().SetPattern(new_token);
            }
        }
    }
}
