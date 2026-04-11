namespace Nt.Applications.SyntaxParser.Actions
{
    internal class DeleteNonTerminal(ApplicationContext context) : ProgramAction(context)
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
            Console.WriteLine("Enter the name of the non terminal to delete:");
            string? name = Console.ReadLine();
            if (name == null || name.Trim().Length == 0)
            {
                Console.WriteLine("Invalid name for a non terminal. Operation cancelled.");
                Context.Automaton.Pop(true);
                return;
            }
            if (!Context.Grammar.NonTerminals.Contains(name))
            {
                Console.WriteLine($"Non terminal '{name}' does not exist in the grammar.");
                Context.Automaton.Pop(true);
                return;
            }
            Context.Grammar.NonTerminals.Remove(name);
            Console.WriteLine($"Non terminal '{name}' deleted successfully.");
            Context.Automaton.Pop(true);
        }
    }
}
