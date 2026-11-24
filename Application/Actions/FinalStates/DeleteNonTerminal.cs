using Nt.Automaton.States;
using Nt.Syntax.Builders;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class DeleteNonTerminal(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Console.WriteLine("Enter the name of the non terminal to delete:");
                string? name = Console.ReadLine();
                if (name == null || name.Trim().Length == 0)
                {
                    Console.WriteLine("Invalid name for a non terminal. Operation cancelled.");
                    return;
                }
                if (!grammar.NonTerminals.Contains((t) => t.Name == name))
                {
                    Console.WriteLine($"Non terminal '{name}' does not exist in the grammar.");
                    return;
                }
                grammar.GetBuilder().RemoveNonTerminal(name);
                Console.WriteLine($"Non terminal '{name}' deleted successfully.");
            }
        }
    }
}
