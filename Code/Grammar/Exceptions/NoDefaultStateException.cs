using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Exceptions
{
    public class NoDefaultStateException : Exception
    {
        public NoDefaultStateException() : base("Default state is not defined") { }
    }
}
