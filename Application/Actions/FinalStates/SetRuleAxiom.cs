using Nt.Syntax.Structures;
using Nt.Syntax.Builders;
using Nt.Automaton.States;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class SetRuleAxiom(ApplicationContext context, Rule rule) : ProgramAction(context)
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
           if (GetGrammar(out var grammar))
            {
                Console.WriteLine("Enter the axiom for the rule");
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    return false;
                }
                try
                {
                    if (grammar.NonTerminals.TryGet((t) => t.Symbol.Name == input, out var nt))
                    {
                        Rule.GetBuilder().SetAxiom(nt.Symbol.Name, -1);
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