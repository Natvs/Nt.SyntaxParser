using Nt.Automaton.Actions;
using Nt.Automaton.States;
using Nt.Automaton.Tokens;
using Nt.Automaton.Transitions;
using Nt.Automaton.Transitions.Strings;
using Nt.Syntax.Actions;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Blocks
{
    public static class SyntaxParserExtensions
    {

        internal class BlockAction(Grammar grammar, Action<Grammar, string> action) : ITokenAction<string>
        {
            private Grammar Grammar { get; } = grammar;
            private Action<Grammar, string> Action { get; } = action;

            public void Perform(IAutomatonToken<string> token)
            {
                Action(Grammar, token.Value);
            }
        }

        extension(SyntaxParser parser)
        {
            public SyntaxBlock CreateBlock(string pattern, string expected)
            {
                var block = parser.Automaton.CreateBlock(pattern);
                block.OpenState
                    .SetDefault(
                        new Transition<string>(block.CloseState)
                        .SetAction(new ExpectedAction(expected)
                    )
                );
                return block;
            }

            public void SetTransition(IState<string> origin, IState<string> target, string pattern)
            {
                if (parser == null) return;
                var transition = new StringTransition(target).AddPattern(pattern);
                origin.AddTransition(transition);
            }

            public void SetTransition(IState<string> origin, IState<string> target, string pattern, Action<Grammar, string> action)
            {
                var transition = 
                    new StringTransition(target)
                    .AddPattern(pattern)
                    .SetAction(
                        new BlockAction(parser.Grammar, action)
                    );
                origin.AddTransition(transition);
            }
        }

    }
}
