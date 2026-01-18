using Nt.Syntax.Structures;
using Nt.Parsing.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class AddNewRuleActionTest
    {
        [Fact]
        public void AddNewRuleAction_Test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var tokens = new SymbolsList(["A"]);
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

            var tokens = new SymbolsList(["B"]);
            var action = new AddNewRuleAction(grammar, tokens);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(0, 0)); });
        }
    }
}
