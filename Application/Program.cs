// See https://aka.ms/new-console-template for more information
using GrammarParser.Syntax;
using GrammarParser.Syntax.Structures;
internal class Program
{
    private static void Main(string[] args)
    {
        bool continue_parsing = true;

        while (continue_parsing)
        {
            string? text = null;
            string input = "";
            var generator = new SyntaxParser();
            Console.WriteLine("Enter text to generate grammar");
            while (text != "end")
            {
                text = Console.ReadLine();
                if (text != "end") input += text + "\n";
            }
            Grammar grammar = generator.ParseString(input);
            Console.WriteLine(grammar.ToString());

            continue_parsing = false;
            Console.WriteLine();
            Console.WriteLine("Continue parsing new grammar?");
            string? answer = Console.ReadLine();
            if (answer == null) return;
            if (answer.ToLower().Equals("y") || answer.ToLower().Equals("yes")) continue_parsing = true;
        }
    }
}
