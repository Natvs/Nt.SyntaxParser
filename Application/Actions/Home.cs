using Nt.Automaton.States;
using Nt.Automaton.Transitions;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class Home(ApplicationContext context) : ProgramAction(context)
    {
        public override State<string> GetState()
        {
            var creationState = new GrammarCreation(Context).GetState();
            var loaderState = new GrammarLoader(Context).GetState();
            var editState = new GrammarEditor(Context).GetState();

            var newState = base.GetState();
            newState.AddTransition(new Transition<string>("1", creationState));
            newState.AddTransition(new Transition<string>("2", loaderState));
            newState.AddTransition(new Transition<string>("3", editState));

            return newState;
        }

        public override void Perform()
        {
            Console.WriteLine("Welcome to Nt Syntax Analyser!");
            Console.WriteLine("Select a program to execute");
            Console.WriteLine("1. Write new grammar");
            Console.WriteLine("2. Load existing grammar");
            Console.WriteLine("3. Edit current grammar");
            Console.WriteLine("4. Exit");
        }
    }

}
