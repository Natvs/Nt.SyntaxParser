using GrammarParser.Domain.Parsing.Structures;
using GrammarParser.Domain.Syntax.Actions;
using GrammarParser.Domain.Syntax.Structures;


namespace GrammarParserTest.Domain.Syntax.Actions
{
    public class AddNewRegExActionTest
    {
        [Fact]
        public void AddNewRegExAction_test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var tokens = new TokensList(["A"]);
            var action = new AddNewRegExAction(grammar, tokens);
            var regex = action.Perform(null, new ParsedToken(0, 0));

            Assert.NotNull(regex);
            Assert.NotNull(regex.Token);
            Assert.Equal("A", tokens[regex.Token.Index].Name);
        }

        [Fact]
        public void AddNewRegexAction_test2()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var tokens = new TokensList(["B"]);
            var action = new AddNewRegExAction(grammar, tokens);

            Assert.Throws<GrammarParser.Domain.Syntax.Exceptions.NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(0, 0)); });
        }
    }
}
