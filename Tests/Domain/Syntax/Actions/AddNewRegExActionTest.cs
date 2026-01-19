using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class AddNewRegExActionTest
    {
        [Fact]
        public void AddNewRegExAction_test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["A"]);
            var action = new AddNewRegExAction(grammar);
            var regex = action.Perform(null, new ParsedToken(symbols.Get(0), 0));

            Assert.NotNull(regex);
            Assert.NotNull(regex.Token);
            Assert.Equal("A", grammar.NonTerminals.Get(regex.Token.Index).Name);
        }

        [Fact]
        public void AddNewRegexAction_test2()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["B"]);
            var action = new AddNewRegExAction(grammar);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(null, new ParsedToken(symbols.Get(0), 0)); });
        }
    }
}
