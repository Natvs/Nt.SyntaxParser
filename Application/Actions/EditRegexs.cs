using Nt.Automaton.States;
using Nt.Automaton.Transitions;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class EditRegexs(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            var state = base.GetState();
            var selectionState = new SelectRegex(Context).GetState();
            var addState = new AddRegex(Context).GetState();

            state.AddTransition(new Transition<string>("1", selectionState));
            state.AddTransition(new Transition<string>("2", addState));

            return state;
        }

        public override void Perform()
        {
            Transition();

            Console.WriteLine("1. Select a regular expression to edit");
            Console.WriteLine("2. Add a new regular expression");
            Console.WriteLine("3. Back");
        }
    }
}
