using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Actions
{
    public class AddRuleDerivationActionTest
    {
        [Fact]
        public void AddRuleDerivationAction_AddTerminalTest()
        {
            var symbols = new SymbolsList(["S", "a"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("a");

            var newaction = new AddNewRuleAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRuleDerivationAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRule(context.Rule, "S", ["a"]);
            Assert.IsType<Terminal>(context.Rule?.Derivation[0]);
        }

        [Fact]
        public void AddRuleDerivationAction_AddNonTerminalTest()
        {
            var symbols = new SymbolsList(["S", "A"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["S", "A"]);

            var newaction = new AddNewRuleAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRuleDerivationAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRule(context.Rule, "S", ["A"]);
            Assert.IsType<NonTerminal>(context.Rule?.Derivation[0]);
        }

        [Fact]
        public void AddRuleDerivationAction_AddUnregisteredSymbolTest()
        {
            var symbols = new SymbolsList(["S", "a"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var newaction = new AddNewRuleAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRuleDerivationAction(grammar, context);
            Assert.Throws<UnknownSymbolException>(() => action.Perform(new AutomatonToken(symbols.Get(1), 0)));
        }

        [Fact]
        public void AddRuleDerivationAction_NullRuleTest()
        {
            var symbols = new SymbolsList(["S", "a"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();

            var action = new AddRuleDerivationAction(grammar, context);
            Assert.Throws<NullRuleException>(() => action.Perform(new AutomatonToken(symbols.Get(1), 0)));
        }

        [Fact]
        public void AddRuleDerivationAction_EscapeCharacterTest1()
        {
            var symbols = new SymbolsList(["S", "'a"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("a");

            var newaction = new AddNewRuleAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRuleDerivationAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRule(context.Rule, "S", ["a"]);
            Assert.IsType<Terminal>(context.Rule?.Derivation[0]);
        }

        [Fact]
        public void AddRuleDerivationAction_EscapeCharacterTest2()
        {
            var symbols = new SymbolsList(["S", "a'b"]);
            var context = new AutomatonContext();
            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("ab");

            var newaction = new AddNewRuleAction(grammar, context);
            newaction.Perform(new AutomatonToken(symbols.Get(0), 0));

            var action = new AddRuleDerivationAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(1), 0));

            AssertRule(context.Rule, "S", ["ab"]);
            Assert.IsType<Terminal>(context.Rule?.Derivation[0]);
        }
    }
}
