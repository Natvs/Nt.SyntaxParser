using Nt.Automaton.States;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class GrammarSaver(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }

        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                Clear();

                Console.WriteLine("Enter the file to save the grammar:");
                var fileName = Console.ReadLine();
                if (fileName == null)
                {
                    Console.Write("Please enter a valid file name. Operation cancelled");
                    return;
                }
                if (File.Exists(fileName))
                {
                    Console.WriteLine($"File '{fileName}' already exists. Overwrite? (y/n)");
                    var overwrite = Console.ReadLine();
                    if (!string.Equals(overwrite, "y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Save operation cancelled.");
                        return;
                    }
                }

                Save(grammar, fileName);
                Console.WriteLine("Grammar saved successfully!");
            }
        }
    } 
   
}
