using Nt.Syntax.Structures;
using Nt.Parsing.Structures;
using Nt.Syntax.Actions;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class SetEscapeCharActionTest
    {
        [Fact]
        public void SetEscapeCharAction_Test()
        {
            var grammar = new Grammar();
            Assert.Equal('\'', grammar.EscapeCharacter);

            var tokens = new SymbolsList(["#"]);
            var action = new SetEscapeCharAction(grammar, tokens);
            action.Perform(new ParsedToken(0, 0));
            Assert.Equal('#', grammar.EscapeCharacter);
        }

    }
}
