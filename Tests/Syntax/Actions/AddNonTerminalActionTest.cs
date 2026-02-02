using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Syntax.Actions;
using Nt.Syntax.Structures;
using Nt.Parser.Symbols;

namespace Nt.Tests.Syntax.Actions
{
    public class AddNonTerminalActionTest
    {
        private SymbolFactory SymbolFactory = new();

        [Fact]
        public void AddNonTerminalAction_Test1()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["A"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentNonTerminalAction(context);
            var writeAction = new AddNonTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.NonTerminals.GetSymbols());
            Assert.Equal("A", grammar.NonTerminals.Get(0).Name);
        }

        [Fact]
        public void AddNonTerminalAction_Test2()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["A", "B"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentNonTerminalAction(context);
            var writeAction = new AddNonTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(1), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.NonTerminals.GetSymbols());
            Assert.Equal("AB", grammar.NonTerminals.Get(0).Name);
        }

        [Fact]
        public void AddNonTerminalAction_EscapeCharacterTest1()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["'", "A"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentNonTerminalAction(context);
            var writeAction = new AddNonTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(1), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.NonTerminals.GetSymbols());
            Assert.Equal("A", grammar.NonTerminals.Get(0).Name);
        }

        [Fact]
        public void AddNonTerminalAction_EscapeCharacterTest2()
        {
            var grammar = new Grammar();
            var symbols = new SymbolsList(SymbolFactory, ["A", "'", "B"]);
            var context = new AutomatonContext();

            var readAction = new AppendToCurrentNonTerminalAction(context);
            var writeAction = new AddNonTerminalAction(grammar, context);

            readAction.Perform(new AutomatonToken(symbols.Get(0), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(1), 0));
            readAction.Perform(new AutomatonToken(symbols.Get(2), 0));
            writeAction.Perform(new AutomatonToken(symbols.Get(0), 0));

            Assert.Single(grammar.NonTerminals.GetSymbols());
            Assert.Equal("AB", grammar.NonTerminals.Get(0).Name);
        }
    }
}
