using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;
using Nt.Parser.Symbols;

namespace Nt.Tests.Syntax.Actions
{
    public class ErrorActionTest
    {
        private SymbolFactory SymbolFactory = new SymbolFactory();

        [Fact]
        public void ErrorAction_Test()
        {
            var symbols = new SymbolsList(SymbolFactory, ["a"]);
            var action = new ErrorAction();

            Assert.Throws<SyntaxError>(() => action.Perform(new AutomatonToken(symbols.Get(0), 1)));
        }
    }
}
