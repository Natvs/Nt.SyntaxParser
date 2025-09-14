using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Parser.Exceptions
{
    public class EmptySymbolException : Exception
    {

        public EmptySymbolException() : base("Symbol should not be empty") { }

    }
}
