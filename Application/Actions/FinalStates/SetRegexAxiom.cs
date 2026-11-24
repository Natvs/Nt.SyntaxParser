using Nt.Automaton.States;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;
using System.Xml.Linq;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class SetRegexAxiom(ApplicationContext context, RegularExpression regex) : ProgramAction(context)
    {
        private RegularExpression Regex { get; set; } = regex;

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
            if (GetGrammar(out var grammar))
            {
                Console.WriteLine("Enter the axiom for the regular expression");
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    return false;
                }

                try
                {
                    if (grammar.NonTerminals.TryGet((t) => t.Name == input, out var nt))
                    {
                        Regex.GetBuilder().SetAxiom(input, -1);
                    }
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Symbol '{input}' is not a non-terminal of the current grammar. Please try again.");
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}