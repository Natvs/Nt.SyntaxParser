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
            var nonTerminalsState = new EditNonTerminals(Context).GetState();
            var termianlsState = new EditTerminals(Context).GetState();
            var axiomState = new AxiomSetter(Context).GetState();
            var rulesState = new EditRules(Context).GetState();
            var regexState = new EditRegexs(Context).GetState();

            state.AddTransition(new Transition<string>("1", nonTerminalsState));
            state.AddTransition(new Transition<string>("2", termianlsState));
            state.AddTransition(new Transition<string>("3", axiomState));
            state.AddTransition(new Transition<string>("4", rulesState));
            state.AddTransition(new Transition<string>("5", regexState));

            return state;
        }

        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Transition();
                Context.Automaton.Pop(true);
                return;
            }

            Console.WriteLine("Current grammar:");
            Console.WriteLine(Context.Grammar.ToString());

            Console.WriteLine("1. Edit list of non terminals");
            Console.WriteLine("2. Edit list of terminals");
            Console.WriteLine("3. Edit axiom");
            Console.WriteLine("4. Edit rules");
            Console.WriteLine("5. Edit regular expressions");
            Console.WriteLine("6. Exit");
        }
    }
}
