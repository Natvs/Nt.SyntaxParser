using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Exportation;
using Nt.Applications.SyntaxParser.Actions.FinalStates;

namespace Nt.Applications.SyntaxParser.Actions.IntermediatesStates
{
    internal class EditNonTerminals(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            var state = base.GetState().SetFinal().OnCondition(() => Context.Grammar == null);
            var addState = new AddNonTerminal(Context).GetState();
            var deleteState = new DeleteNonTerminal(Context).GetState();

            state.AddTransition(new Transition<string>("1", addState));
            state.AddTransition(new Transition<string>("2", deleteState));

            return state;
        }

        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Clear();
                Console.WriteLine("Current non terminals:");
                Console.WriteLine(grammar.NonTerminals.ToString(ExportationMode.Original));
                Console.WriteLine();
                Console.WriteLine("1. Add a non terminal");
                Console.WriteLine("2. Remove a non terminal");
                Console.WriteLine("3. Exit");
            }
        }
    }
}