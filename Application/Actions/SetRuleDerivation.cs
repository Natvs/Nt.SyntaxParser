using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using Nt.Automaton.States;

namespace Nt.Applications.SyntaxParser.Actions
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
            foreach (var token in tokens)
            {
                if (Context.Grammar.Terminals.TryGet((t) => t.Symbol.Name == token, out var t))
                {
                    builder.Add(t);
                }
                else if (Context.Grammar.NonTerminals.TryGet((t) => t.Symbol.Name == token, out var nt))
                {
                    builder.Add(nt);
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