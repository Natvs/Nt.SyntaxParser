using Nt.Syntax.Structures;

namespace Nt.Syntax.Exceptions
{
    public class RuleNotFoundException(Rule rule, string message) : InternalException(message)
    {
        public Rule Rule { get; } = rule;
    }
}
