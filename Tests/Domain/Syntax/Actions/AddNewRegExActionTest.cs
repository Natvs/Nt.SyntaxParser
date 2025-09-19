using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax.Actions;
using GrammarParser.Syntax.Structures;
using GrammarParser.Syntax.Exceptions;


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

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(0, 0)); });
        }
    }
}
