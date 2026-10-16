using Nt.Automaton.Events;
using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Structures;

namespace Nt.Applications.SyntaxParser.Actions
{
    internal class SelectRegex(ApplicationContext context) : ProgramAction(context)
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
            for (int i = 0; i < Context.Grammar.RegularExpressions.Count; i++)
            {
                List<RegularExpression> regexs = [.. Context.Grammar.RegularExpressions];
                var target = new EditRegex(Context, regexs[i]).GetState();
                state.OverwriteTransition(new Transition<string>($"{i + 1}", target));
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

            // Display the list of regular expressions in the current grammar
            List<RegularExpression> regexs = [.. Context.Grammar.RegularExpressions];
            Console.WriteLine("Select a regular expression to edit:");
            for (int i = 0; i < regexs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {regexs[i]}");
            }
            Console.WriteLine($"{regexs.Count + 1}. Cancel");
        }

    }
}
