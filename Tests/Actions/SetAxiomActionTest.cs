using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Actions;
using Nt.Syntax.Automaton;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;

namespace Nt.Tests.Syntax.Actions
{
    public class SetAxiomActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void SetAxiomAction_Test1()
        {
            var symbols = new SymbolsList(SymbolFactory, ["A", "B", "C"]);
            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["A", "B", "C"]);

            var action = new SetAxiomAction(grammar);
            action.Perform(new AutomatonToken(symbols.Get(2), 1));

            Assert.NotNull(grammar.Axiom);
            Assert.Equal("C", grammar.Axiom.Name);
        }

        [Fact]
        public void SetAxiomAction_Test2()
        {
            var symbols = new SymbolsList(SymbolFactory, ["A", "B", "C"]);
            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["A", "B"]);

            var action = new SetAxiomAction(grammar);
            Assert.Throws<UnregisteredNonTerminalException>(() => action.Perform(new AutomatonToken(symbols.Get(2), 1)));
        }
    }
}
