using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class EditNonTerminals(ApplicationContext context) : ProgramAction(context)
    {
        public override State<string> GetState()
        {
            var state = base.GetState();
            var addState = new AddNonTerminal(Context).GetState();
            var deleteState = new DeleteNonTerminal(Context).GetState();

            state.AddTransition(new Transition<string>("1", addState));
            state.AddTransition(new Transition<string>("2", deleteState));

            return state;
        }

        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Context.Automaton.Pop(true);
                return;
            }

            Console.WriteLine("Current non terminals:");
            foreach (var non_terminal in Context.Grammar.NonTerminals.GetSymbols())
            {
                Console.WriteLine(non_terminal);
            }

            Console.WriteLine();
            Console.WriteLine("1. Add a non terminal");
            Console.WriteLine("2. Remove a non terminal");
            Console.WriteLine("3. Exit");
        }
    }
}