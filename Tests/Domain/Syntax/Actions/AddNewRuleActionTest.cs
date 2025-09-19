using GrammarParser.Syntax.Actions;
using GrammarParser.Syntax.Structures;
using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Exceptions;

namespace GrammarParserTest.Domain.Syntax.Actions
{
    public class AddNewRuleActionTest
    {
        [Fact]
        public void AddNewRuleAction_Test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var tokens = new TokensList(["A"]);
            var action = new AddNewRuleAction(grammar, tokens);
            var rule = action.Perform(null, new ParsedToken(0, 0));

            Assert.NotNull(rule);
            Assert.NotNull(rule.Token);
            Assert.Equal("A", tokens[rule.Token.Index].Name);
        }

        [Fact]
        public void AddNewRuleAction_Test2()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var tokens = new TokensList(["B"]);
            var action = new AddNewRuleAction(grammar, tokens);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(0, 0)); });
        }
    }
}
