using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions.IntermediatesStates
{
    internal partial class SelectRule : ProgramAction
    {
        private IState<string> State { get; }

        public SelectRule(ApplicationContext context) : base(context)
        {
            State = base.GetState();
            State.Activated += OnActivated;
        }

        public override IState<string> GetState()
        {
            return State;
        }

        private void OnActivated(object? sender, EventArgs e)
        {
            if (Context.Grammar == null) return;

            State.ClearTransitions();
            for (int i = 0; i < Context.Grammar.Rules.Count; i++)
            {
                List<Rule> rules = [.. Context.Grammar.Rules];
                var target = new EditRule(Context, rules[i]).GetState();
                State.AddTransition(new Transition<string>($"{i+1}", target));
            }
        }

        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                List<Rule> rules = [.. grammar.Rules];
                Console.WriteLine("Select a rule to edit:");
                for (int i = 0; i < rules.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {rules[i]}");
                }
                Console.WriteLine($"{rules.Count + 1}. Cancel");
            }
        }
    }
}
