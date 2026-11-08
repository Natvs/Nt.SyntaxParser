using Nt.Automaton.Actions;
using Nt.Automaton.Tokens;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Branching
{
    internal class ParserTokenAction(AutomatonContext context, Action<AutomatonContext, IAutomatonToken<string>> action) : ITokenAction<string>
    {
        protected AutomatonContext Context { get; } = context;
        protected Action<AutomatonContext, IAutomatonToken<string>> Action { get; } = action;

        public void Perform(IAutomatonToken<string> token)
        {
            Action(Context, token);
        }
    }
}
