using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Exceptions
{

    public class RegisteredTerminalException : Exception
    {

        public RegisteredTerminalException(string name) : base($"Terminal {name} is already registered") { }

    }
}
