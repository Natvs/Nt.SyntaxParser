using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Automaton.Transitions.Strings;
using Nt.Syntax.Actions;

namespace Nt.Syntax
{
    public class SyntaxBlock
    {
        // Patterns
        private string _pattern = "";

        public string Pattern
        {
            get => _pattern;
            set
            {
                _pattern = value;
                Patterns = [.. value.Split(' ')];
            }
        }
        private List<string> Patterns { get; set; } = [];

        // States

        /// <summary>
        /// Represent the start of the block
        /// </summary>
        public IState<string> OpenState { get; private set; } = new State<string>();

        /// <summary>
        /// Represent the end of the block
        /// </summary>
        public IState<string> CloseState { get; private set; } = new State<string>();

        private IState<string> FirstState { get; set; } = new State<string>();

        /// <summary>
        /// Create a new block starting with the following pattern.
        /// </summary>
        /// <remarks>The pattern allows white spaces and <see cref="System.Text.RegularExpressions.Regex"/> regular expressions.</remarks>
        /// <param name="pattern"></param>
        public SyntaxBlock(string pattern)
        {
            Pattern = pattern;

            // Create the intermediate pattern states
            var intermediates = new List<IState<string>>();
            for (int i = 0; i < Patterns.Count; i++)
            {
                intermediates.Add(new State<string>());
            }
            FirstState = intermediates[0];

            // Link the intermediate states together
            for (int i = 0; i < Patterns.Count - 1; i++)
            {
                intermediates[i]
                    .SetDefault(
                        new Transition<string>(CloseState)
                        .SetAction(new ExpectedAction(Patterns[i+1]))
                    )
                    .AddTransition(
                        new StringTransition(intermediates[i + 1])
                        .AddPattern(Patterns[i+1])
                    );
            }

            // Link the last intermediate state to the open state
            intermediates[^1]
                .SetDefault(new Transition<string>(CloseState).SetAction(new ExpectedAction("(")))
                .AddTransition(new StringTransition(OpenState).Include("("));
        }

        internal void Link(IState<string> initial, IState<string> final)
        {
            initial
                .AddTransition(
                    new StringTransition(FirstState)
                    .AddPattern(Patterns[0])
                );
            CloseState
                .SetDefault(
                    new StringTransition(final)
                    .SetAction(new ExpectedAction(")"))
                )
                .AddTransition(
                    new StringTransition(final)
                    .Include(")")
                );
        }

    }
}
