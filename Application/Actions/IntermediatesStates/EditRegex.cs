using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Structures;
using Nt.Applications.SyntaxParser.Actions.FinalStates;

namespace Nt.Applications.SyntaxParser.Actions.IntermediatesStates
{
    internal class EditRegex(ApplicationContext context, RegularExpression regex) : ProgramAction(context)
    {
        private RegularExpression Regex { get; set; } = regex;

        public override IState<string> GetState()
        {
            var axiomState = new SetRegexAxiom(Context, Regex).GetState();
            var patternState = new SetRegexPattern(Context, Regex).GetState();
            var deleteState = new DeleteRegex(Context, Regex).GetState();

            var state = new State<string>().SetAction(this);
            state.AddTransition(new Transition<string>("1", axiomState));
            state.AddTransition(new Transition<string>("2", patternState));
            state.AddTransition(new Transition<string>("3", deleteState));

            return state;
        }

        public override void Perform()
        {
            Clear();
            Console.WriteLine($"Editing regular expression: {Regex}");
            Console.WriteLine("");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Edit regular expression axiom");
            Console.WriteLine("2. Set regular expression pattern");
            Console.WriteLine("3. Delete regular expression");
            Console.WriteLine("4. Back");
        }
    }
}
