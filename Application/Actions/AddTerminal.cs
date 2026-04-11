namespace Nt.Applications.SyntaxParser.Actions
{
    internal class AddTerminal(ApplicationContext context) : ProgramAction(context)
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
            Console.WriteLine("Enter the name of the terminal to add:");
            string? name = Console.ReadLine();
            if (name == null || name.Trim().Length == 0)
            {
                Console.WriteLine("Invalid name for a terminal. Operation cancelled.");
                Context.Automaton.Pop(true);
                return;
            }
            if (Context.Grammar.Terminals.Contains(name))
            {
                Console.WriteLine($"A terminal '{name}' already exists in the grammar.");
                Context.Automaton.Pop(true);
                return;
            }
            Context.Grammar.Terminals.Add(name);
            Console.WriteLine($"Terminal '{name}' added successfully.");
            Context.Automaton.Pop(true);
        }
    }
}