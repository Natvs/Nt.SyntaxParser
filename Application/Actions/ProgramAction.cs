using System;
using System.Collections.Generic;
using System.Text;
using Nt.Applications.SyntaxParser;
using Nt.Automaton.Actions;

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

        public abstract void Perform();
    }
}
