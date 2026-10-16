using Nt.Automaton.Actions;
using Nt.Automaton.States;
using Nt.Automaton.Tokens;
using Nt.Automaton.Transitions;
using Nt.Syntax.Automaton;

namespace Nt.Syntax.Branching
{
    internal class ParserAction(AutomatonContext context, Action<AutomatonContext> action) : IAction
    {
        protected AutomatonContext Context { get; } = context;
        protected Action<AutomatonContext> Action { get; } = action;

        public void Perform()
        {
            Action(Context);
        }
    }

    internal class ParserTokenAction(AutomatonContext context, Action<AutomatonContext, IAutomatonToken<string>> action) : ITokenAction<string>
    {
        protected AutomatonContext Context { get; } = context;
        protected Action<AutomatonContext, IAutomatonToken<string>> Action { get; } = action;

        public void Perform(IAutomatonToken<string> token)
        {
            Action(Context, token);
        }
    }

    public static class BranchingExtensions
    {

        /// <summary>
        /// Begin a new branch when a specified token is read.
        /// </summary>
        /// <param name="token">Token to read to jump on the new branch</param>
        /// <param name="state">State the automaton jumps to after reading the token</param>
        public static void StartBranch(this SyntaxParser parser, string token, IState<string> state)
        {
            parser.Automaton?.StartBranch(token, state);
        }

        /// <summary>
        /// End a branch by linking it to the default state
        /// </summary>
        /// <param name="state">Terminal state of the branch to link to the default state</param>
        /// <param name="token">Token to read to trigger the linking</param>
        public static void EndBranch(this SyntaxParser parser, IState<string> state, string token)
        {
            parser.Automaton?.EndBranch(state, token);
        }

        /// <summary>
        /// Associate an action to this state to be executed when the state is reached.
        /// </summary>
        /// <param name="state">State to associate the action with</param>
        /// <param name="parser">Parser providing the current context of the parsing</param>
        /// <param name="action">Action to execute when the state is activated</param>
        /// <returns>The same IState<string> as provided, for method chaining.</returns>
        public static IState<string> SetAction(this IState<string> state, SyntaxParser parser, Action<AutomatonContext> action)
        {
            return state.SetAction(new ParserAction(parser.Automaton.Context, action));
        }

        /// <summary>
        /// Associate an action the this transition to be executed when the transition is performed.
        /// </summary>
        /// <param name="transition">Transition to associate the action with</param>
        /// <param name="parser">Parser providing the current context of the parsing</param>
        /// <param name="action">Action to execute when the transition is performed</param>
        /// <returns>The same ITransition<string> as provided, for method chaining.</returns>
        public static ITransition<string> SetAction(this ITransition<string> transition, SyntaxParser parser, Action<AutomatonContext, IAutomatonToken<string>> action)
        {
            return transition.SetAction(new ParserTokenAction(parser.Automaton.Context, action));
        }

    }
}
