using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Actions;
using GrammarParser.Syntax.Structures;

namespace Tests.Domain.Syntax.Actions
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
