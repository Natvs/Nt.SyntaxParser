using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Actions;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;

namespace Nt.Tests.Syntax.Actions
{
    public class SetEscapeCharActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void SetEscapeCharAction_Test()
        {
            var grammar = new Grammar();
            Assert.Equal('\'', grammar.EscapeCharacter);

            var symbols = new SymbolsList(SymbolFactory, ["#"]);
            var action = new SetEscapeCharAction(grammar);
            action.Perform(new AutomatonToken(symbols.Get(0), 0));
            Assert.Equal('#', grammar.EscapeCharacter);
        }

    }
}
