using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Exportation;
using Nt.Applications.SyntaxParser.Actions.FinalStates;

namespace Nt.Applications.SyntaxParser.Actions.IntermediatesStates
{
    internal class EditTerminals(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            var state = base.GetState().SetFinal().OnCondition(() => Context.Grammar == null);
            var addState = new AddTerminal(Context).GetState();
            var deleteState = new DeleteTerminal(Context).GetState();
            state.AddTransition(new Transition<string>("1", addState));
            state.AddTransition(new Transition<string>("2", deleteState));
            return state;
        }
        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Clear();
                Console.WriteLine("Current terminals:");
                Console.WriteLine(grammar.Terminals.ToString(ExportationMode.Original));
                Console.WriteLine();
                Console.WriteLine("1. Add a terminal");
                Console.WriteLine("2. Remove a terminal");
                Console.WriteLine("3. Exit");
            }
        }
    }
}
