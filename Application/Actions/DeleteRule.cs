using Nt.Syntax.Structures;
using System.Text.RegularExpressions;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class DeleteRule(ApplicationContext context, Rule rule) : ProgramAction(context)
    {
        private Rule Rule { get; set; } = rule;
        public override void Perform()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Context.Automaton.Pop(true);
                return;
            }
            Context.Grammar.Rules.Remove(Rule);
            Console.WriteLine($"Rule {Rule} has been removed");
            Context.Automaton.Pop(false);
            Context.Automaton.Pop(true);
        }
    }
}