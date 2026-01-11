// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information
using Nt.Syntax.Structures;
using Nt.Syntax;
using Nt.Parsing;
internal class Program
{
    private static void Main(string[] args)
    {
        TrySyntaxParsing();
    }

    private static void TrySyntaxParsing()
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
            try
            {
                Grammar grammar = generator.ParseString(input);
                Console.WriteLine("\nParsed grammar:\n" + grammar.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError while parsing grammar:\n" + ex.Message);
            }

            continue_parsing = false;
            Console.WriteLine();
            Console.WriteLine("Continue parsing new grammar?");
            string? answer = Console.ReadLine();
            if (answer == null) return;
            if (answer.ToLower().Equals("y") || answer.ToLower().Equals("yes")) continue_parsing = true;
        }
    }

    private static void TryParsing()
    {
        bool continue_parsing = true;
        while (continue_parsing)
        {
            string? text = null;
            string input = "";
            var parser = new Parser([' ', '\0', '\n', '\t'], [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"]);
            Console.WriteLine("Enter text to parse (end to finish):");
            while (text != "end")
            {
                text = Console.ReadLine();
                if (text != "end") input += text + "\n";
            }
            try
            {
                ParserResult result = parser.Parse(input);
                Console.WriteLine("\nParsed result:\n" + result.Parsed.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError while parsing:\n" + ex.Message);
            }

            continue_parsing = false;
            Console.WriteLine();
            Console.WriteLine("Continue parsing new text?");
            string? answer = Console.ReadLine();
            if (answer == null) return;
            if (answer.ToLower().Equals("y") || answer.ToLower().Equals("yes")) continue_parsing = true;
        }
    }
}

