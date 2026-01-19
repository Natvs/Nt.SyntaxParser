using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class AddRuleDerivationActionTest
    {
        [Fact]
        public void AddRuleDerivationAction_AddTerminalTest()
        {
            var symbols = new SymbolsList(["S", "a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("a");

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar);
            var newrule = action.Perform(rule, new(symbols.Get(1), 0));

            Assert.Equal(rule, newrule);
            Assert.Single(rule.Derivation);
            Assert.IsType<Terminal>(rule.Derivation[0]);
            Assert.Equal("a", grammar.Terminals.Get(rule.Derivation[0].Index).Name);
            rule.Derivation.ToString();
        }

        [Fact]
        public void AddRuleDerivationAction_AddNonTerminalTest()
        {
            var symbols = new SymbolsList(["S", "A"]);

            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["S", "A"]);

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar);
            var newrule = action.Perform(rule, new(symbols.Get(1), 0));

            Assert.Equal(rule, newrule);
            Assert.Single(rule.Derivation);
            Assert.IsType<NonTerminal>(rule.Derivation[0]);
            Assert.Equal("A", grammar.NonTerminals.Get(rule.Derivation[0].Index).Name);
        }

        [Fact]
        public void AddRuleDerivationAction_AddUnregisteredSymbolTest()
        {
            var symbols = new SymbolsList(["S", "a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar);
            Assert.Throws<UnknownSymbolException>(() => action.Perform(rule, new ParsedToken(symbols.Get(1), 0)));
        }

        [Fact]
        public void AddRuleDerivationAction_NullRuleTest()
        {
            var symbols = new SymbolsList(["S", "a"]);
            var grammar = new Grammar();

            var action = new AddRuleDerivationAction(grammar);
            Assert.Throws<NullRuleException>(() => action.Perform(null, new(symbols.Get(1), 0)));
        }

        [Fact]
        public void AddRuleDerivationAction_EscapeCharacterTest1()
        {
            var symbols = new SymbolsList(["S", "'a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("a");

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar);
            action.Perform(rule, new ParsedToken(symbols.Get(1), 0));
            Assert.Single(rule.Derivation);
            Assert.IsType<Terminal>(rule.Derivation[0]);
            Assert.Equal("a", grammar.Terminals.Get(rule.Derivation[0].Index).Name);
        }

        [Fact]
        public void AddRuleDerivationAction_EscapeCharacterTest2()
        {
            var symbols = new SymbolsList(["S", "a'b"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("ab");

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar);
            action.Perform(rule, new ParsedToken(symbols.Get(1), 0));
            Assert.Single(rule.Derivation);
            Assert.IsType<Terminal>(rule.Derivation[0]);
            Assert.Equal("ab", grammar.Terminals.Get(rule.Derivation[0].Index).Name);
        }
    }
}
