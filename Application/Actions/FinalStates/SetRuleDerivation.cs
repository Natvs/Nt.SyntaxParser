using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using Nt.Automaton.States;
using Nt.Syntax.Exceptions;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class SetRuleDerivation(ApplicationContext context, Rule rule) : ProgramAction(context)
    {
        private Rule Rule { get; set; } = rule;

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
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                return false;
            }
            Console.WriteLine("Enter the derivation for the rule (insert ' ' between each symbol)");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return false;
            }

            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var builder = Rule.GetBuilder().Clear();
            try
            {
                foreach (var token in tokens)
                {
                    builder.Add(token, -1);
                }
            }
            catch (UnknownSymbolException ex)
            {
                Console.WriteLine($"Unknown symbol {ex.Name} found in the derivation. Please try again.");
            }
            return true;
        }
    }
}