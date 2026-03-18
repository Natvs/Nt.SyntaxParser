using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Actions;
using Nt.Syntax.Structures;
using Nt.Parser.Symbols;

namespace Nt.Tests.Syntax.Actions
{
    public class AddTerminalActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void AddTerminalAction_Test1()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["a"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentTerminalAction(context);
            var writeAction = new AddTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.Terminals.GetSymbols());
            Assert.Equal("a", grammar.Terminals.Get(0).Name);
        }

        [Fact]
        public void AddTerminalAction_Test2()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["a", "b"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentTerminalAction(context);
            var writeAction = new AddTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(1), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.Terminals.GetSymbols());
            Assert.Equal("ab", grammar.Terminals.Get(0).Name);
        }

        [Fact]
        public void AddTerminalAction_EscapeCharacterTest1()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["'", "a"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentTerminalAction(context);
            var writeAction = new AddTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(1), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.Terminals.GetSymbols());
            Assert.Equal("a", grammar.Terminals.Get(0).Name);
        }

        [Fact]
        public void AddTerminalAction_EscapeCharacterTest2()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["a", "'", "b"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentTerminalAction(context);
            var writeAction = new AddTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(1), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(2), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.Terminals.GetSymbols());
            Assert.Equal("ab", grammar.Terminals.Get(0).Name);
        }
    }
}
