using Nt.Automaton.States;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class DeleteRegex(ApplicationContext context, RegularExpression regex) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        private RegularExpression Regex { get; set; } = regex;
        public override void Perform()
        {
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                return;
            }
            Context.Grammar.GetBuilder().Remove(Regex);
            Console.WriteLine($"Regular expression {Regex} has been removed");
            Context.Automaton.Pop();
        }
    }
}