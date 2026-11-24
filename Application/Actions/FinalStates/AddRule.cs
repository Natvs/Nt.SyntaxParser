using Nt.Automaton.States;
using Nt.Syntax.Builders;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class AddRule(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            return base.GetState().SetFinal();
        }
        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                if (grammar.NonTerminals.Count < 1)
                {
                    Console.WriteLine("The current grammar has no non-terminals. Please add a non-terminal first.");
                    return;
                }

                grammar.GetBuilder().AddRule(grammar.NonTerminals[0].Name, -1, out var rule);

                var axiomAction = new SetRuleAxiom(Context, rule);
                var derivationAction = new SetRuleDerivation(Context, rule);

                axiomAction.Prompt();
                derivationAction.Prompt();

                Console.WriteLine($"The new rule {rule} has been added to the grammar");
            }
        }
    }
}