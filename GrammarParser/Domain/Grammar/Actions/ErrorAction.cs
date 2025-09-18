using GrammarParser.Domain.Grammar.Exceptions;
using GrammarParser.Domain.Parser.Structures;

namespace GrammarParser.Domain.Grammar.Actions
{
    public class ErrorAction(TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(tokens[word.TokenIndex].Name, word.Line);
        }

    }
}
