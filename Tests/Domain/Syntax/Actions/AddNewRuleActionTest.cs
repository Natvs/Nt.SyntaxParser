using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;

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
            var context = new AutomatonContext();
            var action = new AddNewRuleAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.NotNull(context.Rule);
            Assert.NotNull(context.Rule.Token);
            Assert.Equal("A", context.Rule.Token.Name);
        }

        [Fact]
        public void AddNewRuleAction_Test2()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["B"]);
            var context = new AutomatonContext();
            var action = new AddNewRuleAction(grammar, context);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(new AutomatonToken(symbols.Get(0), 0)); });
        }
    }
}
