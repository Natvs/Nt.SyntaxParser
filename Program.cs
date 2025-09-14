// See https://aka.ms/new-console-template for more information
using GrammarReader.Code.Class;
using GrammarReader.Code.Parser;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        string? text = null;
        string input = "";
        var generator = new GrammarReader.Code.Grammar.Generator();
        Console.WriteLine("Enter text to generate grammar");
        while (text != "end")
        {
            text = Console.ReadLine();
            if (text != "end") input += text + "\n";
        }
        Grammar grammar = generator.Generate(input);
        Console.WriteLine(grammar.ToString());
        Console.ReadLine();
    }
}