using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;

namespace Nt.Syntax.Actions
{
    public class SetEscapeCharAction(Grammar grammar) : Action()
    {
        public override void Perform(ParsedToken word)
        {
            if (word.Symbol.Name.Length != 1)
            {
                throw new InvalidEscapeCharSymbolException(word.Symbol.Name, word.Line);
            }
            grammar.EscapeCharacter = word.Symbol.Name.ToCharArray()[0];
        }
    }

}
