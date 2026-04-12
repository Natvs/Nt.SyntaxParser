using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal partial class SelectRule(ApplicationContext context) : ProgramAction(context)
    {

        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                return;
            }

            // Display the list of rules in the current grammar
            List<Rule> rules = [.. Context.Grammar.Rules];
            Console.WriteLine("Select a rule to edit:");
            for (int i = 0; i < rules.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rules[i]}");
            }
            Console.WriteLine($"{rules.Count + 1}. Cancel");
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
            if (ruleIndex > 0 && ruleIndex <= rules.Count)
            {
                var state = new EditRule(Context, rules[ruleIndex - 1]).GetState();
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
