using GrammarParser.Domain.Syntax.Exceptions;
using GrammarParser.Domain.Parsing.Structures;

namespace GrammarParser.Domain.Syntax.Actions
{
    public class ErrorAction(TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(tokens[word.TokenIndex].Name, word.Line);
        }

    }
}
