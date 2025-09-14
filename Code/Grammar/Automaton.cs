using GrammarReader.Code.Class;
using GrammarReader.Code.Grammar.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar
{

    public class Automaton(TokensList tokens, State initialState)
    {
        public State Current { get; private set; } = initialState;

        public void Read(ParsedToken token)
        {
            if (Current == null) return;
            Current = Current.Read(token, tokens);
        }
    }
}
