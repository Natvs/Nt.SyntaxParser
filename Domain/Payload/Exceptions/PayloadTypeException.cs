using Nt.Syntax.Structures;

namespace Nt.Syntax.Payload.Exceptions
{
    public class PayloadTypeException : Exception
    {
        public PayloadTypeException(Terminal terminal, Type expectedType) : base($"The payload of the terminal {terminal} is not of type {expectedType.Name}") { }
        public PayloadTypeException(NonTerminal nonterminal, Type expectedType) : base($"The payload of the non-terminal {nonterminal} is not of type {expectedType.Name}") { }
        public PayloadTypeException(Rule rule, Type expectedType) : base($"The payload of the rule {rule} is not of type {expectedType.Name}") { }
        public PayloadTypeException(RegularExpression regex, Type expectedType) : base($"The payload of the regular expression {regex} is not of type {expectedType.Name}") { }
    }
}
