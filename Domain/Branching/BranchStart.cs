using Nt.Automaton.States;

namespace Nt.Syntax.Branching
{
    internal class BranchStart(string token, IState<string> target)
    {
        public string Token { get; } = token;
        public IState<string> Target { get; } = target;
    }
    
}
