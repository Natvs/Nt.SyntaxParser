using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Parser.Exceptions
{
    public class UnregisteredSymbolException : Exception
    {
        public UnregisteredSymbolException(string symbol) : base($"Symbol {symbol} is not registered") { }
    }
}
