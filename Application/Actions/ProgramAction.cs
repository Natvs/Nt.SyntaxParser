using System;
using System.Collections.Generic;
using System.Text;
using Nt.Applications.SyntaxParser;
using Nt.Automaton.Actions;
using Nt.Automaton.States;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal abstract class ProgramAction(ApplicationContext context) : IAction
    {
        protected ApplicationContext Context { get; } = context;

        protected static void Transition()
        {
            Console.WriteLine();
            Console.WriteLine("- - - - - - - - - -");
            Console.WriteLine();
        }

        public virtual IState<string> GetState()
        {
            return new State<string>().SetAction(this);
        }

        public abstract void Perform();
    }
}
