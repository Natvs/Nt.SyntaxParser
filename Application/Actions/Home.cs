using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.SyntaxParser.Application;
using Nt.SyntaxParser.Application.Actions;

namespace Nt.Syntax.Programs
{
    internal class Home(ApplicationContext context) : ProgramAction(context)
    {
        public static State<string> GetState(ApplicationContext context)
        {
            var creationState = new State<string>(new GrammarCreation(context));
            var loaderState = new State<string>(new GrammarLoader(context));

            var newState = new State<string>(new Home(context));
            newState.AddTransition(new Transition<string>("1", creationState));
            newState.AddTransition(new Transition<string>("2", loaderState));

            return newState;
        }

        public override void Perform()
        {
            Console.WriteLine("Welcome to Nt Syntax Analyser!");
            Console.WriteLine("Select a program to execute");
            Console.WriteLine("1. Write new grammar");
            Console.WriteLine("2. Load existing grammar");
            Console.WriteLine("3. Exit");
        }
    }

}
