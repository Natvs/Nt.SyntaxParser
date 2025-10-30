using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions.Tests
{
    public class AddNonTerminalActionTest
    {
        [Fact]
        public void AddNonTerminalAction_Test()
        {
            var grammar = new Grammar();
            var tokens = new TokensList(["A"]);
            var action = new AddNonTerminalAction(grammar, tokens);
            action.Perform(new ParsedToken(0, 0));

            Assert.Single(grammar.NonTerminals);
            Assert.Equal("A", grammar.NonTerminals[0].Name);
        }
    }
}
