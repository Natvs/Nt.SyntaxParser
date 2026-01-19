using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    public class ErrorAction() : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(word.Symbol.Name, word.Line);
        }

    }
}
