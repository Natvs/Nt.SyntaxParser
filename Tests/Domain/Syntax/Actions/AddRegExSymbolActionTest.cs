using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class AddRegExSymbolActionTest
    {
        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest1()
        {
            var symbols = new SymbolsList(["S", "ab*"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar);
            regex.SetToken(new(symbols.Get(0), -1));

            var action = new AddRegExSymbolsAction(grammar);
            var newregex = action.Perform(regex, new(symbols.Get(1), 0));

            Assert.Equal(regex, newregex);
            Assert.Equal("ab*", regex.Pattern);
        }

        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest2()
        {
            var symbols = new SymbolsList(["S", "a", "+", "(", "bc", ")", "*"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar);
            regex.SetToken(new(symbols.Get(0), -1));

            var action = new AddRegExSymbolsAction(grammar);
            RegularExpression? newregex = regex;
            for (int i = 1; i < symbols.GetCount(); i++)
            {
                newregex = action.Perform(newregex, new(symbols.Get(i), 0));
            }

            Assert.Equal(regex, newregex);
            Assert.Equal("a+(bc)*", regex.Pattern);
        }

        [Fact]
        public void AddRegExSymbolAction_NullRegexTest()
        {
            var symbols = new SymbolsList(["S", "(ab)+"]);

            var grammar = new Grammar();

            var action = new AddRegExSymbolsAction(grammar);
            Assert.Throws<NullRegexException>(() => action.Perform(null, new(symbols.Get(1), 0)));
        }

        [Fact]
        public void AddRegExSymbolAction_EscapeTest1()
        {
            var symbols = new SymbolsList(["S", "\\a"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar);
            regex.SetToken(new(symbols.Get(0), -1));

            var action = new AddRegExSymbolsAction(grammar);
            var newregex = action.Perform(regex, new(symbols.Get(1), 0));

            Assert.Equal(regex, newregex);
            Assert.Equal("\\a", regex.Pattern);
        }
    }
}
