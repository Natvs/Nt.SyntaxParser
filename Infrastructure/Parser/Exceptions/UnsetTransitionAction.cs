namespace GrammarReader.Infrastructure.Parser.Exceptions
{
    public class UnsetTransitionAction : Exception
    {
        public UnsetTransitionAction(string action) : base($"Transition action {action} is not set in automaton") { }
    }
}
