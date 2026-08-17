using Nt.Automaton.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Syntax.Branching
{
    internal class BranchStart(string token, IState<string> target)
    {
        public string Token { get; } = token;
        public IState<string> Target { get; } = target;
    }
    
}
