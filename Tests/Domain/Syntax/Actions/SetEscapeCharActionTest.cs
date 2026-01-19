using Nt.Syntax.Structures;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class SetEscapeCharActionTest
    {
        [Fact]
        public void SetEscapeCharAction_Test()
        {
            var grammar = new Grammar();
            Assert.Equal('\'', grammar.EscapeCharacter);

            var symbols = new SymbolsList(["#"]);
            var action = new SetEscapeCharAction(grammar);
            action.Perform(new ParsedToken(symbols.Get(0), 0));
            Assert.Equal('#', grammar.EscapeCharacter);
        }

    }
}
