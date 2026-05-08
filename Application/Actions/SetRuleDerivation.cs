using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class SetRuleDerivation(ApplicationContext context, Rule rule) : ProgramAction(context)
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
            Console.WriteLine("Enter the derivation for the rule (insert ' ' between each symbol)");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return false;
            }

            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                if (Context.Grammar.Terminals.Contains(token))
                {
                    Rule.GetBuilder().Add(new Terminal(Context.Grammar.Terminals.Get(token), -1));
                }
                else if (Context.Grammar.NonTerminals.Contains(token))
                {
                    Rule.GetBuilder().Add(new NonTerminal(Context.Grammar.NonTerminals.Get(token), -1));
                }
                else
                {
                    Console.WriteLine($"Symbol '{token}' is not a symbol of the current grammar. Please try again.");
                    return false;
                }
            }
            return true;
        }
    }
}