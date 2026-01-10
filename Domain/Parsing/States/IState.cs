using Nt.Parsing.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.SyntaxParser.Parsing.States
{
    internal interface IState 
    {
       void Handle(char c);
    }
}
