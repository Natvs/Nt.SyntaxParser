using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class GrammarEditor(ApplicationContext context) : ProgramAction(context)
    {
        public override State<string> GetState()
        {
            var state = base.GetState();
            var nonTerminalsState = new NonTerminalsEditor(Context).GetState();
            var termianlsState = new TerminalsEditor(Context).GetState();

            state.AddTransition(new Transition<string>("1", nonTerminalsState));
            state.AddTransition(new Transition<string>("2", termianlsState));

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
            Console.WriteLine("Current grammar:");
            Console.WriteLine(Context.Grammar.ToString());
            Transition();

            Console.WriteLine("1. Edit list of non terminals");
            Console.WriteLine("2. Edit list of terminals");
            Console.WriteLine("3. Edit list axiom");
            Console.WriteLine("4. Edit rules");
            Console.WriteLine("5. Edit regular expressions");
        }
    }
}
