using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Exceptions
{
    public class SyntaxError : Exception
    {

        public SyntaxError(int line) : base($"Syntax error in grammar at line {line}") { }

    }
}
