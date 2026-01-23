using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Actions;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;

namespace Nt.Tests.Syntax.Actions
{
    public class AddNewRegExActionTest
    {
        [Fact]
        public void AddNewRegExAction_test1()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["A"]);
            var context = new AutomatonContext();
            var action = new AddNewRegExAction(grammar, context);
            action.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.NotNull(context.Regex);
            Assert.NotNull(context.Regex.Token);
            Assert.Equal("A", context.Regex.Token.Name);
        }

        [Fact]
        public void AddNewRegexAction_test2()
        {
            var grammar = new Grammar();
            grammar.NonTerminals.Add("A");

            var symbols = new SymbolsList(["B"]);
            var context = new AutomatonContext();
            var action = new AddNewRegExAction(grammar, context);

            Assert.Throws<NotDeclaredNonTerminalException>(() => { action.Perform(new AutomatonToken(symbols.Get(0), 0)); });
        }
    }
}
