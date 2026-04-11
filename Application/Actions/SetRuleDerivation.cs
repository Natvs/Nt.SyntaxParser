using Nt.Automaton.States;
using Nt.Syntax.Structures;

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
                    Rule.Derivation.Add(new Terminal(Context.Grammar.Terminals.Get(token), -1));
                }
                else if (Context.Grammar.NonTerminals.Contains(token))
                {
                    Rule.Derivation.Add(new NonTerminal(Context.Grammar.NonTerminals.Get(token), -1));
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

    internal class AddRule(ApplicationContext context) : ProgramAction(context)
    {
        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Context.Automaton.Pop(true);
                return;
            }
            var rule = new Rule(Context.Grammar);

            var axiomAction = new SetRuleAxiom(Context, rule);
            var derivationAction = new SetRuleDerivation(Context, rule);

            var valid = true;
            if (valid) valid = axiomAction.Prompt();
            if (valid) valid = derivationAction.Prompt();

            if (valid)
            {
                Context.Grammar.AddRule(rule);
                Console.WriteLine($"The new rule {rule} has been added to the grammar");
            }
            else Console.WriteLine("The rule was not added to the grammar due to invalid input.");
            Context.Automaton.Pop(true);
        }
    }
}