using Nt.Syntax.Exceptions;
using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions
{
    public class ErrorAction(TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(tokens[word.TokenIndex].Name, word.Line);
        }

    }
}
