using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Actions;
using GrammarParser.Syntax.Exceptions;
using GrammarParser.Syntax.Structures;

namespace Tests.Domain.Syntax.Actions
{
    public class AddRegExSymbolActionTest
    {
        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest1()
        {
            var tokens = new TokensList(["S", "ab*"]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar.NonTerminals);
            regex.SetToken(0, 0);

            var action = new AddRegExSymbolsAction(tokens);
            var newregex = action.Perform(regex, new(1, 0));

            Assert.Equal(regex, newregex);
            Assert.Equal("ab*", regex.RegExString);
        }

        [Fact]
        public void AddRegExSymbolAction_AddSymbolTest2()
        {
            var tokens = new TokensList(["S", "a", "+", "(", "bc", ")", "*" ]);

            var grammar = new Grammar();
            grammar.NonTerminals.Add("S");

            var regex = new RegularExpression(grammar.NonTerminals);
            regex.SetToken(0, 0);

            AddRegExSymbolsAction action = new AddRegExSymbolsAction(tokens);
            RegularExpression? newregex = regex;
            for (int i = 1; i < tokens.Count; i++)
            {
                newregex = action.Perform(newregex, new(i, 0));
            }

            Assert.Equal(regex, newregex);
            Assert.Equal("a+(bc)*", regex.RegExString);
        }

        [Fact]
        public void AddRegExSymbolAction_NullRegexTest()
        {
            var tokens = new TokensList(["S", "(ab)+"]);

            var action = new AddRegExSymbolsAction(tokens);
            Assert.Throws<NullRegexException>(() => action.Perform(null, new(1, 0)));
        }
    }
}
