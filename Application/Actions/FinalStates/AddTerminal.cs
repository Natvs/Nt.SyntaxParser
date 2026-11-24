using Nt.Automaton.States;
using Nt.Syntax.Builders;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class AddTerminal(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Console.WriteLine("Enter the name of the terminal to add:");
                string? name = Console.ReadLine();
                if (name == null || name.Trim().Length == 0)
                {
                    Console.WriteLine("Invalid name for a terminal. Operation cancelled.");
                    return;
                }
                if (grammar.Terminals.Contains((t) => t.Name == name))
                {
                    Console.WriteLine($"A terminal '{name}' already exists in the grammar.");
                    return;
                }
                grammar.GetBuilder().AddTerminal(name);
                Console.WriteLine($"Terminal '{name}' added successfully.");
            }
        }
    }
}