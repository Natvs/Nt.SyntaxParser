using Nt.Automaton.States;
using Nt.Automaton.Transitions;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class TerminalsEditor(ApplicationContext context) : ProgramAction(context)
    {
        public override State<string> GetState()
        {
            var state = base.GetState();
            var addState = new AddTerminal(Context).GetState();
            var deleteState = new DeleteTerminal(Context).GetState();
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
            Console.WriteLine("Current terminals:");
            foreach (var terminal in Context.Grammar.Terminals.GetSymbols())
            {
                Console.WriteLine(terminal);
            }
            
            Console.WriteLine();
            Console.WriteLine("1. Add a terminal");
            Console.WriteLine("2. Remove a terminal");
            Console.WriteLine("3. Exit");
        }
    }
}
