using Nt.Automaton.States;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class GrammarCreation(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }

        public override void Perform()
        {
            Transition();

            string? text = null;
            string input = "";
            var generator = new Syntax.SyntaxParser();
            Console.WriteLine("Enter text to generate grammar. Write a new line 'end' when done with the grammar.");

            // Ask the user to fill the grammar
            while (text != "end")
            {
                text = Console.ReadLine();
                if (text != "end") input += text + "\n";
            }
            try
            {
                // Creates a new grammar from the user's input
                Context.Grammar = generator.ParseString(input);
                Console.WriteLine();
                Console.WriteLine("Grammar successfully created.");

                // Displays the parsed grammar
                Console.WriteLine("Created grammar:\n" + Context.Grammar.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the grammar: " + ex.Message);
            }
        }
    }

}
