using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Parser.Exceptions
{
    public class RegisteredSymbolException : Exception
    {
        public RegisteredSymbolException(string symbol) : base($"Symbol {symbol} already registered") { }
    }
}
