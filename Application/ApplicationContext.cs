using Nt.Automaton.Automatons;
using Nt.Syntax;
using Nt.Syntax.Programs;
using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.SyntaxParser.Application
{
    internal class ApplicationContext
    {
        public ApplicationContext()
        {
            Automaton.Push(Home.GetState(this), true);
        }

        public StackAutomaton<string> Automaton { get; set; } = new StackAutomaton<string>().SetAutoPerformAction();

        public Grammar? Grammar { get; set; } = null;
    }
}
