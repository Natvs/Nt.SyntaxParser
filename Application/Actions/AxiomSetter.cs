using Nt.Automaton.States;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class AxiomSetter(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                return;
            }
            if (Context.Grammar.NonTerminals.Count == 0)
            {
                Console.WriteLine("There are no non terminals in the grammar. Please add some non terminals first.");
                return;
            }

            Console.WriteLine("Current non terminals:");
            foreach (var non_terminal in Context.Grammar.NonTerminals.GetSymbols())
            {
                Console.WriteLine(non_terminal);
            }
            if (Context.Grammar.Axiom == null) Console.WriteLine("No current axiom");
            else Console.WriteLine($"Current axiom: {Context.Grammar.Axiom}");

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
                var symbol = Context.Grammar.NonTerminals.Get(name);
                Context.Grammar.GetBuilder().SetAxiom(new NonTerminal(symbol, -1));
            }
            catch (KeyNotFoundException) 
            {
                Console.WriteLine($"There are no non terminal '{name}' in the grammar.");
            }
        }
    }
}
