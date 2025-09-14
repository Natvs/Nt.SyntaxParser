using GrammarReader.Code.Class;
using GrammarReader.Code.Grammar.Actions;
using GrammarReader.Code.Grammar.Exceptions;

namespace GrammarReader.Code.Grammar
{
    public class State
    {
        public List<Transition> Transitions { get; } = [];
        public State? DefaultState { get; private set; }
        public Actions.Action? DefaulAction { get; private set; }
        public Actions.Action? Action { get; }

        public State() { }
        public State(Actions.Action action)
        {
            this.Action = action;
        }

        public State SetDefault(State defaultState)
        {
            this.DefaultState = defaultState;
            return this;
        }
        public State SetDefault(State defaultState, Actions.Action defaultAction)
        {
            this.DefaultState = defaultState;
            this.DefaulAction = defaultAction;
            return this;
        }

        public void AddTransition(string value, State state)
        {
            this.Transitions.Add(new Transition(value, state));
        }
        public void AddTransition(string value, State state, Actions.Action action)
        {
            this.Transitions.Add(new Transition(value, state, action));
        }

        public State Read(ParsedToken token, TokensList tokens)
        {
            if (DefaultState == null) throw new NoDefaultStateException();
            foreach (var transition in Transitions)
            {
                if (transition.Value.Equals(tokens[token.Value].Name))
                {
                    transition.Action?.Perform(token);
                    transition.NewState.Action?.Perform(token);
                    return transition.NewState;
                }
            }
            this.DefaulAction?.Perform(token);
            return this.DefaultState;
        }
    }
}
