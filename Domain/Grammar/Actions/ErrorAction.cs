using GrammarReader.Domain.Grammar.Exceptions;
using GrammarReader.Infrastructure.Parser.Structures;

namespace GrammarReader.Domain.Grammar.Actions
{
    public class ErrorAction(TokensList tokens) : Action
    {

        public override void Perform(ParsedToken word)
        {
            throw new SyntaxError(tokens[word.TokenIndex].Name, word.Line);
        }

    }
}
