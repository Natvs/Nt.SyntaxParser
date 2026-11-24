using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using Nt.Automaton.States;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class SetRegexPattern(ApplicationContext context, RegularExpression regex) : ProgramAction(context)
    {
        RegularExpression Regex { get; set; } = regex;

        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }

        public override void Perform()
        {
            Console.WriteLine();
            Prompt();
        }

        public bool Prompt()
        {
            Console.WriteLine("Enter the pattern for the regular expression:");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return false;
            }

            Regex.GetBuilder().SetPattern(input);
            return true;
        }
    }
}