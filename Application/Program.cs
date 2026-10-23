using Nt.Applications.SyntaxParser;
internal class Program
{
    private static void Main(string[] args)
    {
        var context = new ApplicationContext();
        Directory.CreateDirectory("grammars");

        // Iterate until the user escapes from the initial state
        context.Automaton.CurrentState?.Activate();
        while (!context.Automaton.IsEmpty)
        {
            var answer = Console.ReadLine();
            if (answer == null) continue;
            context.Automaton.Read(new ApplicationToken(answer));
        }
    }

}

