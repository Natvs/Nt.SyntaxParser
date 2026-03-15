using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class NonTerminalsEditor(ApplicationContext context) : ProgramAction(context)
    {
        public override State<string> GetState()
        {
            var state = base.GetState();
            var addState = new AddNonTerminal(context).GetState();
            var deleteState = new DeleteNonTerminal(context).GetState();

            state.AddTransition(new Transition<string>("1", addState));
            state.AddTransition(new Transition<string>("2", deleteState));

            return state;
        }

        public override void Perform()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Transition();
                Context.Automaton.Pop(true);
                return;
            }

            Transition();
            Console.WriteLine("Current non terminals:");
            foreach (var non_terminal in Context.Grammar.NonTerminals.GetSymbols())
            {
                Console.WriteLine(non_terminal);
            }
            Transition();

            Console.WriteLine("1. Add a non terminal");
            Console.WriteLine("2. Remove a non terminal");
            Console.WriteLine("3. Exit");
        }
    }
}
