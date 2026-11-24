using Nt.Automaton.States;
using Nt.Syntax.Builders;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class AxiomSetter(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                if (grammar.NonTerminals.Count == 0)
                {
                    Console.WriteLine("There are no non terminals in the grammar. Please add some non terminals first.");
                    return;
                }

                Clear();
                Console.WriteLine("Current non terminals:");
                Console.WriteLine(grammar.NonTerminals.ToString());
                Console.WriteLine();
                if (grammar.Axiom == null) Console.WriteLine("No current axiom");
                else Console.WriteLine($"Current axiom: {grammar.Axiom}");
                Console.WriteLine();
                Console.WriteLine("Enter new axiom:");
                string? name = Console.ReadLine();
                if (name == null || name.Trim().Length == 0)
                {
                    Console.WriteLine("Invalid name for an axiom. Operation cancelled.");
                    return;
                }
                try
                {
                    grammar.GetBuilder().SetAxiom(name, -1);
                }
                catch (AxiomException)
                {
                    Console.WriteLine($"There are no non terminal '{name}' in the grammar.");
                }
            }

        }
    }
}
