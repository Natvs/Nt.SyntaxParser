namespace Nt.Applications.SyntaxParser.Actions
{
    internal class DeleteTerminal(ApplicationContext context) : ProgramAction(context)
    {
        public override void Perform()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Transition();
                Context.Automaton.Pop(true);
                return;
            }
            Console.WriteLine("Enter the name of the terminal to delete:");
            string? name = Console.ReadLine();
            if (name == null || name.Trim().Length == 0)
            {
                Console.WriteLine("Invalid name for a terminal. Operation cancelled.");
                Transition();
                Context.Automaton.Pop(true);
                return;
            }
            if (!Context.Grammar.Terminals.Contains(name))
            {
                Console.WriteLine($"Terminal '{name}' does not exist in the grammar.");
                Transition();
                Context.Automaton.Pop(true);
                return;
            }
            Context.Grammar.Terminals.Remove(name);
            Console.WriteLine($"Terminal '{name}' deleted successfully.");
            Transition();
            Context.Automaton.Pop(true);
        }
    }
}
