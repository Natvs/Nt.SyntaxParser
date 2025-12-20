using Nt.Parsing.Structures;
using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;


namespace Nt.Syntax.Actions.Tests
{
    public class AddNewRegExActionTest
    {
        [Fact]
        public void AddNewRegExAction_test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var tokens = new SymbolsList(["A"]);
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

            var tokens = new SymbolsList(["B"]);
            var action = new AddNewRegExAction(grammar, tokens);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(0, 0)); });
        }
    }
}
