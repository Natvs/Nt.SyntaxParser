using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Parsing.Structures;

namespace Nt.Syntax.Actions.Tests
{
    public class AddRegExSymbolActionTest
    {
        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest1()
        {
            var tokens = new SymbolsList(["S", "ab*"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar.NonTerminals);
            regex.SetToken(0, 0);

            var action = new AddRegExSymbolsAction(grammar, tokens);
            var newregex = action.Perform(regex, new(1, 0));

            Assert.Equal(regex, newregex);
            Assert.Equal("ab*", regex.Pattern);
        }

        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest2()
        {
            var tokens = new SymbolsList(["S", "a", "+", "(", "bc", ")", "*"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar.NonTerminals);
            regex.SetToken(0, 0);

            var action = new AddRegExSymbolsAction(grammar, tokens);
            RegularExpression? newregex = regex;
            for (int i = 1; i < tokens.Count; i++)
            {
                newregex = action.Perform(newregex, new(i, 0));
            }

            Assert.Equal(regex, newregex);
            Assert.Equal("a+(bc)*", regex.Pattern);
        }

        [Fact]
        public void AddRegExSymbolAction_NullRegexTest()
        {
            var tokens = new SymbolsList(["S", "(ab)+"]);

            var grammar = new Grammar();

            var action = new AddRegExSymbolsAction(grammar, tokens);
            Assert.Throws<NullRegexException>(() => action.Perform(null, new(1, 0)));
        }

        [Fact]
        public void AddRegExSymbolAction_EscapeTest1()
        {
            var tokens = new SymbolsList(["S", "\\a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar.NonTerminals);
            regex.SetToken(0, 0);

            var action = new AddRegExSymbolsAction(grammar, tokens);
            var newregex = action.Perform(regex, new(1, 0));

            Assert.Equal(regex, newregex);
            Assert.Equal("\\a", regex.Pattern);
        }
    }
}
