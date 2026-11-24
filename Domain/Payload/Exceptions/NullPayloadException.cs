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
}
