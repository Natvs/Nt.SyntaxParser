using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class AddNewRuleActionTest
    {
        [Fact]
        public void AddNewRuleAction_Test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["A"]);
            var action = new AddNewRuleAction(grammar);
            var rule = action.Perform(null, new ParsedToken(symbols.Get(0), 0));

            Assert.NotNull(rule);
            Assert.NotNull(rule.Token);
            Assert.Equal("A", symbols.Get(rule.Token.Index).Name);
        }

        [Fact]
        public void AddNewRuleAction_Test2()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["B"]);
            var action = new AddNewRuleAction(grammar);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(symbols.Get(0), 0)); });
        }
    }
}
