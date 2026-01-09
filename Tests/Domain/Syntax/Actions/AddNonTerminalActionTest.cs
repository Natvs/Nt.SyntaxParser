using Nt.Parsing.Structures;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Actions.Tests
{
    public class AddNonTerminalActionTest
    {
        [Fact]
        public void AddNonTerminalAction_Test1()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(["A"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentNonTerminalAction(symbols, context);
            var writeAction = new AddNonTerminalAction(grammar, context);

            readAction.Perform(new ParsedToken(0, 0));
            writeAction.Perform(new ParsedToken(0, 0));

            Assert.Single(grammar.NonTerminals);
            Assert.Equal("A", grammar.NonTerminals[0].Name);
        }

        [Fact]
        public void AddNonTerminalAction_Test2()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(["A", "B"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentNonTerminalAction(symbols, context);
            var writeAction = new AddNonTerminalAction(grammar, context);

            readAction.Perform(new ParsedToken(0, 0));
            readAction.Perform(new ParsedToken(1, 0));
            writeAction.Perform(new ParsedToken(0, 0));

            Assert.Single(grammar.NonTerminals);
            Assert.Equal("AB", grammar.NonTerminals[0].Name);
        }
    }
}
