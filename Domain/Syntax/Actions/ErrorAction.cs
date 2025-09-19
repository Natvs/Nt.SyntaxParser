using GrammarParser.Syntax.Exceptions;
using GrammarParser.Parsing.Structures;

namespace GrammarParser.Syntax.Actions
{
    public class ErrorAction(TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(tokens[word.TokenIndex].Name, word.Line);
        }

    }
}
