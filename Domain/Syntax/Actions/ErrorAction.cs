using Nt.SyntaxParser.Syntax.Exceptions;
using Nt.SyntaxParser.Parsing.Structures;

namespace Nt.SyntaxParser.Syntax.Actions
{
    public class ErrorAction(TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(tokens[word.TokenIndex].Name, word.Line);
        }

    }
}
