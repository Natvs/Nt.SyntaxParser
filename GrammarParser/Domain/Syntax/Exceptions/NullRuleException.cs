namespace GrammarParser.Domain.Syntax.Exceptions
{
    public class NullRuleException : Exception
    {
        public NullRuleException(string message) : base(message) { }
    }
}
