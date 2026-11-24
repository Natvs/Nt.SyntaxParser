using Nt.Automaton.States;
using Nt.Syntax.Builders;

namespace Nt.Applications.SyntaxParser.Actions.FinalStates
{
    internal class AddRegex(ApplicationContext context) : ProgramAction(context)
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

                grammar.GetBuilder().AddRegularExpression(grammar.NonTerminals[0].Name, -1, out var regex);

                var axiomAction = new SetRegexAxiom(Context, regex);
                var derivationAction = new SetRegexPattern(Context, regex);

                axiomAction.Prompt();
                derivationAction.Prompt();

                Console.WriteLine($"The new regular expression {regex} has been added to the grammar");
            }
        }
    }
}