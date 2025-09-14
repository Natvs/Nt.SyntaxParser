using GrammarReader.Code.Class;
using GrammarReader.Code.Grammar.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammarReader.Code.Grammar.Actions
{
    public class ErrorAction(Class.Grammar grammar, TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(word.Line);
        }

    }
}
