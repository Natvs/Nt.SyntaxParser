using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Actions;
using Nt.SyntaxParser.Syntax.Structures;
using Nt.SyntaxParser.Syntax.Exceptions;


namespace Nt.SyntaxParser.Tests.Syntax.Actions
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
