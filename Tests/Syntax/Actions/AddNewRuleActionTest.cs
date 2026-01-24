using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;
using Nt.Syntax.Automaton;
using Nt.Parser.Symbols;
using Nt.Parser.Structures;

namespace Nt.Tests.Syntax.Actions
{
    public class AddNewRuleActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void AddNewRuleAction_Test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(SymbolFactory, ["A"]);
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

            var symbols = new SymbolsList(SymbolFactory, ["B"]);
            var context = new AutomatonContext();
            var action = new AddNewRuleAction(grammar, context);

            Assert.Throws<UnregisteredNonTerminalException>(() => { action.Perform(new AutomatonToken(symbols.Get(0), 0)); });
        }
    }
}
