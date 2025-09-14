using GrammarReader.Code.Class;
using GrammarReader.Code.Grammar.Actions;
using GrammarReader.Code.Grammar.Exceptions;

namespace GrammarReader.Code.Grammar
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
            this.Action = action;
        }

        public State SetDefault(State defaultState)
        {
            this.DefaultState = defaultState;
            return this;
        }
        public State SetDefault(State defaultState, IAction defaultAction)
        {
            this.DefaultState = defaultState;
            this.DefaulAction = defaultAction;
            return this;
        }

        public void AddTransition(string value, State state)
        {
            this.Transitions.Add(new Transition(value, state));
        }
        public void AddTransition(string value, State state, IAction action)
        {
            this.Transitions.Add(new Transition(value, state, action));
        }

        /// <summary>
        /// Reads a token and gets the next state
        /// </summary>
        /// <param name="token">Parsed token to read</param>
        /// <param name="tokens">List of parsed tokens as a reference</param>
        /// <returns>Next state of the automaton after reading the token</returns>
        /// <exception cref="NoDefaultStateException">It might be that no default state was set for this state</exception>
        public State Read(ParsedToken token, TokensList tokens, Rule? currentRule, out Rule? newRule)
        {
            newRule = currentRule;
            if (DefaultState == null) throw new NoDefaultStateException();
            foreach (var transition in Transitions)
            {
                if (transition.Value.Equals(tokens[token.Value].Name))
                {
                    newRule = PerformAction(transition.Action, token, newRule);
                    newRule = PerformAction(transition.NewState.Action, token, newRule);                  
                    return transition.NewState;
                }
            }
            newRule = PerformAction(DefaulAction, token, newRule);
            newRule = PerformAction(DefaultState.Action, token, newRule);
            return this.DefaultState;
        }

        private static Rule? PerformAction(IAction? iaction, ParsedToken token, Rule? currentRule)
        {
            if (iaction is Actions.Action action) action.Perform(token);
            else if (iaction is RuleAction ruleAction) return ruleAction.Perform(currentRule, token);
            return currentRule;
        }
    }
}
