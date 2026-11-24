using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Exportation;
using Nt.Applications.SyntaxParser.Actions.FinalStates;

namespace Nt.Applications.SyntaxParser.Actions.IntermediatesStates
{
    internal class EditRules(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            var state = base.GetState();
            var selectionState = new SelectRule(Context).GetState();
            var addState = new AddRule(Context).GetState();

            state.AddTransition(new Transition<string>("1", selectionState));
            state.AddTransition(new Transition<string>("2", addState));

            return state;
        }

        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Clear();
                Console.WriteLine("Current rules:");
                Console.WriteLine(grammar.Rules.ToString(ExportationMode.Original));
                Console.WriteLine();
                Console.WriteLine("1. Select a rule to edit");
                Console.WriteLine("2. Add a new rule");
                Console.WriteLine("3. Back");
            }
        }
    }
}
