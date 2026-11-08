using Nt.Automaton.Actions;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Branching
{
    internal class ParserAction(AutomatonContext context, Action<AutomatonContext> action) : IAction
    {
        protected AutomatonContext Context { get; } = context;
        protected Action<AutomatonContext> Action { get; } = action;

        public void Perform()
        {
            Action(Context);
        }
    }
}
