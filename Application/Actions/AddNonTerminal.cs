using Nt.Automaton.States;
using Nt.Syntax.Builders;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class AddNonTerminal(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }

        public override void Perform()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Transition();
                return;
            }

            Console.WriteLine("Enter the name of the non terminal to add:");
            string? name = Console.ReadLine();
            if (name == null || name.Trim().Length == 0)
            {
                Console.WriteLine("Invalid name for a non terminal. Operation cancelled.");
                return;
            }
            if (Context.Grammar.Terminals.Contains(name))
            {
                Console.WriteLine($"A non terminal '{name}' already exists in the grammar.");
                return;
            }
            Context.Grammar.GetBuilder().AddNonTerminal(name);
            Console.WriteLine($"Non terminal '{name}' added successfully.");
        }
    }
}