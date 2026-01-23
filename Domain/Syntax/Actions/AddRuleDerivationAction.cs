using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using System.Data;

namespace Nt.Syntax.Actions
{
    public class AddRuleDerivationAction(Grammar grammar, AutomatonContext context) : IAction<string>
    {
        public void Perform(IAutomatonToken<string> word)
        {
            if (word is AutomatonToken token)
            {
                if (context.Rule == null) throw new NullRuleException("Attempting to write to a derivation of a non existent rule");

                // Handles escape characters
                var new_token = grammar.ParseToGrammarToken(token);
                context.Rule.Add(new_token);
            }
        }
    }
}
