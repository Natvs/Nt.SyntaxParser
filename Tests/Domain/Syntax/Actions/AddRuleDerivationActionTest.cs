using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions.Tests
{
    public class AddRuleDerivationActionTest
    {
        [Fact]
        public void AddRuleDerivationAction_AddTerminalTest()
        {
            var tokens = new TokensList(["S", "a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");
            grammar.Terminals.Add("a");

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar, tokens);
            var newrule = action.Perform(rule, new(1, 0));

            Assert.Equal(rule, newrule);
            Assert.Single(rule.Derivation);
            Assert.IsType<Terminal>(rule.Derivation[0]);
            Assert.Equal("a", grammar.Terminals[rule.Derivation[0].Index].Name);
            rule.Derivation.ToString();
        }

        [Fact]
        public void AddRuleDerivationAction_AddNonTerminalTest()
        {
            var tokens = new TokensList(["S", "A"]);

            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["S", "A"]);

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar, tokens);
            var newrule = action.Perform(rule, new(1, 0));

            Assert.Equal(rule, newrule);
            Assert.Single(rule.Derivation);
            Assert.IsType<NonTerminal>(rule.Derivation[0]);
            Assert.Equal("A", grammar.NonTerminals[rule.Derivation[0].Index].Name);
        }

        [Fact]
        public void AddRuleDerivationAction_AddUnregisteredSymbolTest()
        {
            var tokens = new TokensList(["S", "a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);
            grammar.Rules.Add(rule);

            var action = new AddRuleDerivationAction(grammar, tokens);
            Assert.Throws<UnknownSymbolException>(() => action.Perform(rule, new ParsedToken(1, 0)));
        }

        [Fact]
        public void AddRuleDerivationAction_NullRuleTest()
        {
            var tokens = new TokensList(["S", "a"]);
            var grammar = new Grammar();

            var action = new AddRuleDerivationAction(grammar, tokens);
            Assert.Throws<NullRuleException>(() => action.Perform(null, new(1, 0)));
        }
    }
}
