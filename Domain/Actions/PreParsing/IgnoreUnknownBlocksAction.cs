using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Actions.PreParsing
{
    internal class IgnoreUnknownBlocksAction(AutomatonContext context) : ITokenAction<string>
    {
        public void Perform(IAutomatonToken<string> token)
        {
            context.IgnoreUnknownBlocks = true;
        }
    }
}
