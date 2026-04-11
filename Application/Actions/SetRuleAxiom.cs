using Nt.Automaton.States;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class SetRuleAxiom(ApplicationContext context, Rule rule) : ProgramAction(context)
    {
        private Rule Rule { get; set; } = rule;


        public override void Perform()
        {
            Console.WriteLine();
            Prompt();
            Context.Automaton.Pop(true);
        }

        public bool Prompt()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                return false;
            }

            Console.WriteLine("Enter the axiom for the rule");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return false;
            }

            try
            {
                var symbol = Context.Grammar.NonTerminals.Get(input);
                Rule.SetToken(new NonTerminal(symbol, -1));
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Symbol '{input}' is not a non-terminal of the current grammar. Please try again.");
                return false;
            }
            return true;
        }
    }
}