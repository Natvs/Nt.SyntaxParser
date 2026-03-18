using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;
using Nt.Syntax.Automaton;
using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Actions
{
    public class AddSameRuleActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void AddSameRuleAction_Test()
        {
            var symbols = new SymbolsList(SymbolFactory, ["A"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var newaction = new AddNewRuleAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddSameRuleAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(0), 0));

            AssertRule(context.Rule, "A", []);
        }

        [Fact]
        public void AddSameRuleAction_EmptyRuleTest()
        {
            var symbols = new SymbolsList(SymbolFactory, ["A"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var action = new AddSameRuleAction(grammar, context);

            Assert.Throws<NullRuleException>(() => { action.Perform(new AutomatonToken(symbols.Get(0), 0)); });
        }
    }
}
