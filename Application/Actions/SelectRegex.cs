using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class SelectRegex(ApplicationContext context) : ProgramAction(context)
    {

        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                return;
            }

            // Display the list of regular expressions in the current grammar
            List<RegularExpression> regexs = [.. Context.Grammar.RegularExpressions];
            Console.WriteLine("Select a regular expression to edit:");
            for (int i = 0; i < regexs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {regexs[i]}");
            }
            Console.WriteLine($"{regexs.Count + 1}. Cancel");
            Console.WriteLine();

            // Prompt the user to select a rule
            string? answer = Console.ReadLine();
            int ruleIndex;
            if (answer == null || !int.TryParse(answer, out ruleIndex))
            {
                Console.WriteLine("Invalid selection. Please enter a valid number.");
                return;
            }

            // Push a new state for editing the selected rule
            if (ruleIndex > 0 && ruleIndex <= regexs.Count)
            {
                var state = new EditRegex(Context, regexs[ruleIndex - 1]).GetState();
                Context.Automaton.Push(state, true);
            }
            else
            {
                Context.Automaton.Pop(true);
                return;
            }

        }

    }
}
