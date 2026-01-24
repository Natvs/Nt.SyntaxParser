using Nt.Syntax.Structures;

namespace Nt.Syntax.Exceptions
{
    public class RuleNotFoundException(Rule rule, string message) : Exception(message)
    {
        public Rule Rule { get; } = rule;
    }
}
