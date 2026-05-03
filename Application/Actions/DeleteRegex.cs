using Nt.Syntax.Structures;
using Nt.Syntax.Builders;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class DeleteRegex(ApplicationContext context, RegularExpression regex) : ProgramAction(context)
    {
        private RegularExpression Regex { get; set; } = regex;
        public override void Perform()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Context.Automaton.Pop(true);
                return;
            }
            Context.Grammar.GetBuilder().Remove(Regex);
            Console.WriteLine($"Regular expression {Regex} has been removed");
            Context.Automaton.Pop(false);
            Context.Automaton.Pop(true);
        }
    }
}