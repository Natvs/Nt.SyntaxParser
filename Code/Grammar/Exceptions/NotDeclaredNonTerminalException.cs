using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Exceptions
{
    internal class NotDeclaredNonTerminalException : Exception
    {

        public NotDeclaredNonTerminalException(string name) : base($"Symbol {name} is not declared as non terminal") { }
    }
}
