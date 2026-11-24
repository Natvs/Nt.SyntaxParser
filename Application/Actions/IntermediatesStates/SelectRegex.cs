using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions.IntermediatesStates
{
    internal class SelectRegex : ProgramAction
    {
        private IState<string> State { get; }

        public SelectRegex(ApplicationContext context) : base(context)
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
            for (int i = 0; i < Context.Grammar.RegularExpressions.Count; i++)
            {
                List<RegularExpression> regexs = [.. Context.Grammar.RegularExpressions];
                var target = new EditRegex(Context, regexs[i]).GetState();
                State.AddTransition(new Transition<string>($"{i + 1}", target));
            }
        }

        public override void Perform()
        {
            if (GetGrammar(out var grammar))
            {
                List<RegularExpression> regexs = [.. grammar.RegularExpressions];
                Console.WriteLine("Select a regular expression to edit:");
                for (int i = 0; i < regexs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {regexs[i]}");
                }
                Console.WriteLine($"{regexs.Count + 1}. Cancel");
            }

        }

    }
}
