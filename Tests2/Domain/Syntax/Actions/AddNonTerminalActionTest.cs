using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Actions;
using Nt.SyntaxParser.Syntax.Structures;

namespace Nt.SyntaxParser.Tests.Syntax.Actions
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
