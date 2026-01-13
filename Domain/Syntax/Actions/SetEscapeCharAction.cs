using Nt.Parsing.Structures;
using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;

namespace Nt.Syntax.Actions
{
    public class SetEscapeCharAction(Grammar grammar, SymbolsList symbols) : Action()
    {
        public override void Perform(ParsedToken word)
        {
            if (symbols[word.TokenIndex].Name.Length != 1)
            {
                throw new InvalidEscapeCharSymbolException(symbols[word.TokenIndex].Name, word.Line);
            }
            grammar.EscapeCharacter = symbols[word.TokenIndex].Name.ToCharArray()[0];
        }
    }

}
