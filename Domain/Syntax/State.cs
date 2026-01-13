using Nt.Parsing.Structures;
using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax
{

    public class State
    {
        public List<Transition> Transitions { get; } = [];
        public State? DefaultState { get; private set; }
        public IAction? DefaulAction { get; private set; }
        public IAction? Action { get; }

        public State() { }
        public State(IAction action)
        {
            Action = action;
        }

        public State SetDefault(State defaultState)
        {
            DefaultState = defaultState;
            return this;
        }
        public State SetDefault(State defaultState, IAction defaultAction)
        {
            DefaultState = defaultState;
            DefaulAction = defaultAction;
            return this;
        }

        public void AddTransition(string value, State state)
        {
            Transitions.Add(new Transition(value, state));
        }
        public void AddTransition(string value, State state, IAction action)
        {
            Transitions.Add(new Transition(value, state, action));
        }

        public void OverwriteTransition(string value, State state)
        {
            List<Transition> toRemove = [];
            foreach (var t in Transitions)
            {
                if (t.Value.Equals(value)) toRemove.Add(t);
            }
            foreach (var t in toRemove)
            {
                Transitions.Remove(t);
            }
            Transitions.Add(new Transition(value, state));
        }
        public void OverwriteTransition(string value, State state, IAction action)
        {
            List<Transition> toRemove = [];
            foreach (var t in Transitions)
            {
                if (t.Value.Equals(value)) toRemove.Add(t);
            }
            foreach (var t in toRemove)
            {
                Transitions.Remove(t);
            }
            Transitions.Add(new Transition(value, state, action));
        }

        public void AddTransitions(List<string> values, State state)
        {
            foreach (string value in values)
            {
                Transitions.Add(new Transition(value, state));
            }
        }
        public void AddTransitions(List<string> values, State state, IAction action)
        {
            foreach (string value in values)
            {
                Transitions.Add(new Transition(value, state, action));
            }
        }

        /// <summary>
        /// Reads a token and gets the next state
        /// </summary>
        /// <param name="token">Parsed token to read</param>
        /// <param name="tokens">List of parsed tokens as a reference</param>
        /// <returns>Next state of the automaton after reading the token</returns>
        /// <remarks>In case of multiple transitions with same symbol, only the first action added will be performed</remarks>
        /// <exception cref="NoDefaultStateException">It might be that no default state was set for this state</exception>
        public State Read(ParsedToken token, SymbolsList tokens, AutomatonContext context)
        {
            if (DefaultState == null) throw new NoDefaultStateException();
            foreach (Transition transition in Transitions)
            {
                if (transition.Value.Equals(tokens[token.TokenIndex].Name))
                {
                    PerformAction(transition.Action, token, context);
                    PerformAction(transition.NewState.Action, token, context);
                    return transition.NewState;
                }
            }
            PerformAction(DefaulAction, token, context);
            PerformAction(DefaultState.Action, token, context);
            return DefaultState;
        }

        private static void PerformAction(IAction? iaction, ParsedToken token, AutomatonContext context)
        {
            if (iaction is Actions.Action action) action.Perform(token);
            else if (iaction is RuleAction ruleAction) context.Rule = ruleAction.Perform(context.Rule, token);
            else if (iaction is RegExAction regexAction) context.RegularExpression = regexAction.Perform(context.RegularExpression, token);
            else if (iaction is ImportFileAction importAction) context.ImportedString = importAction.Perform(token);
        }
    }
}
