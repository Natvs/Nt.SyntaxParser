using Nt.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Syntax.Payload.Exceptions
{
    public class NullPayloadException : Exception
    {
        public NullPayloadException(Terminal terminal) : base($"The terminal {terminal} does not have a payload.") { }

        public NullPayloadException(NonTerminal nonterminal) : base($"The non-terminal {nonterminal} does not have a payload.") { }

        public NullPayloadException(Rule rule) : base($"The rule {rule} does not have a payload.") { }

        public NullPayloadException(RegularExpression regex) : base($"The regular expression {regex} does not have a payload.") { }
    }

    public class PayloadTypeException : Exception
    {
        public PayloadTypeException(Terminal terminal, Type expectedType) : base($"The payload of the terminal {terminal} is not of type {expectedType.Name}") { }
        public PayloadTypeException(NonTerminal nonterminal, Type expectedType) : base($"The payload of the non-terminal {nonterminal} is not of type {expectedType.Name}") { }
        public PayloadTypeException(Rule rule, Type expectedType) : base($"The payload of the rule {rule} is not of type {expectedType.Name}") { }
        public PayloadTypeException(RegularExpression regex, Type expectedType) : base($"The payload of the regular expression {regex} is not of type {expectedType.Name}") { }
    }
}
