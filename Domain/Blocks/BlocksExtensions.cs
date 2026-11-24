using Nt.Automaton.States;
using Nt.Automaton.Transitions;
using Nt.Syntax.Actions;

namespace Nt.Syntax.Blocks
{
    public static class BlocksExtensions
    {

        extension(SyntaxBlock block)
        {
            public State<string> CreateState(string expected)
            {
                var state = new State<string>();
                state
                    .SetDefault(
                        new Transition<string>(block.CloseState)
                        .SetAction(new ExpectedAction(expected)
                    )
                );
                return state;
            }

        }

    }
}
