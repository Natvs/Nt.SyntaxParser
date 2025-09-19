using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarParser.Syntax.Exceptions
{
    public class NullRegexException : Exception
    {
        public NullRegexException(string message) : base(message) { }
    }
}
