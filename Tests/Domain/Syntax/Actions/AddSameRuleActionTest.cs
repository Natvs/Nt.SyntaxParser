using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class AddSameRuleActionTest
    {
        [Fact]
        public void AddSameRuleAction_Test()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var action = new AddSameRuleAction(grammar);
            var rule = new Rule(grammar.Terminals, grammar.NonTerminals);
            rule.SetToken(0, 0);

            var newrule = action.Perform(rule, new(new(""), 0));

            Assert.NotNull(newrule);
            Assert.NotNull(rule.Token);
            Assert.NotNull(newrule.Token);
            Assert.NotEqual(rule, newrule);
            Assert.Equal(rule.Token.Index, newrule.Token.Index);
        }

        [Fact]
        public void AddSameRuleAction_EmptyRuleTest()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var action = new AddSameRuleAction(grammar);

            Assert.Throws<NullRuleException>(() => { action.Perform(null, new(new(""), 0)); });
        }
    }
}
