using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class AddRule(ApplicationContext context) : ProgramAction(context)
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
            var rule = new Rule(Context.Grammar);

            var axiomAction = new SetRuleAxiom(Context, rule);
            var derivationAction = new SetRuleDerivation(Context, rule);

            var valid = true;
            if (valid) valid = axiomAction.Prompt();
            if (valid) valid = derivationAction.Prompt();

            if (valid)
            {
                Context.Grammar.AddRule(rule);
                Console.WriteLine($"The new rule {rule} has been added to the grammar");
            }
            else Console.WriteLine("The rule was not added to the grammar due to invalid input.");
            Context.Automaton.Pop(true);
        }
    }
}