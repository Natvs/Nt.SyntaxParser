using Nt.Automaton.Events;
using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal partial class SelectRule(ApplicationContext context) : ProgramAction(context)
    {
        public override IState<string> GetState()
        {
            var state = base.GetState();
            state.Reach += OnReached;
            return state;
        }

        private void OnReached(object? sender, TransitionEventArgs<string> e)
        {
            if (Context.Grammar == null) return;

            var state = e.Transition.Target;
            for (int i = 0; i < Context.Grammar.Rules.Count; i++)
            {
                List<Rule> rules = [.. Context.Grammar.Rules];
                var target = new EditRule(Context, rules[i]).GetState();
                state.OverwriteTransition(new Transition<string>($"{i+1}", target));
            }
        }

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
        }
    }
}
