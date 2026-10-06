using Nt.Applications.SyntaxParser.Actions;
using Nt.Automaton.Automatons;
using Nt.Syntax;
using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Applications.SyntaxParser
{
    internal class ApplicationContext
    {
        public ApplicationContext()
        {
            Automaton.Push(new Home(this).GetState(), true);
        }

        public StackAutomaton<string> Automaton { get; set; } = new StackAutomaton<string>().SetAutoPerformAction();

        public Grammar? Grammar { get; set; } = null;
    }
}
