using Nt.Automaton.States;

namespace Nt.Syntax.Branching
{
    internal sealed class BranchEnd(IState<string> origin, string token)
    {
        public string Token { get; } = token;
        public IState<string> Origin { get; } = origin;
    }
    
}
