using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class RuleEditor(ApplicationContext context, Rule rule) : ProgramAction(context)
    {
        private Rule Rule { get; set; } = rule;

        public override State<string> GetState()
        {
            var axiomState = new SetRuleAxiom(Context, Rule).GetState();
            var derivationState = new SetRuleDerivation(Context, Rule).GetState();
            var deleteState = new DeleteRule(Context, Rule).GetState();

            var state = new State<string>(this);
            state.AddTransition(new Transition<string>("1", axiomState));
            state.AddTransition(new Transition<string>("2", derivationState));
            state.AddTransition(new Transition<string>("3", deleteState));

            return state;
        }

        public override void Perform()
        {
            Transition();

            Console.WriteLine("1. Edit rule axiom");
            Console.WriteLine("2. Set rule derivation");
            Console.WriteLine("3. Delete rule");
            Console.WriteLine("4. Back");
        }
    }
}
