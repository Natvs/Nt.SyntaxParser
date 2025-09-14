namespace GrammarReader.Code.Grammar
{
    public class Transition
    {
        public string Value { get; }
        public State NewState { get; }
        public Actions.Action? Action { get; }

        public Transition(string value, State newState)
        {
            Value = value;
            NewState = newState;
        }
        public Transition(string value, State newState, Actions.Action action)
        {
            Value = value;
            NewState = newState;
            Action = action;
        }
    }
}
