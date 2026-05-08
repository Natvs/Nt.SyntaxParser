using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class AddRegex(ApplicationContext context) : ProgramAction(context)
    {
        public override void Perform()
        {
            Transition();
            if (Context.Grammar == null)
            {
                Console.WriteLine("No current grammar. Please load or create a grammar first.");
                Context.Automaton.Pop(true);
                return;
            }
            var regex = new RegularExpression(Context.Grammar);

            var axiomAction = new SetRegexAxiom(Context, regex);
            var derivationAction = new SetRegexPattern(Context, regex);

            var valid = true;
            if (valid) valid = axiomAction.Prompt();
            if (valid) valid = derivationAction.Prompt();

            if (valid)
            {
                Context.Grammar.GetBuilder().Add(regex);
                Console.WriteLine($"The new regular expression {regex} has been added to the grammar");
            }
            else Console.WriteLine("The regular expression was not added to the grammar due to invalid input.");
            Context.Automaton.Pop(true);
        }
    }
}