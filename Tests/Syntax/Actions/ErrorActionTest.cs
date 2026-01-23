using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;
using Nt.Syntax.Automaton;

namespace Nt.Tests.Syntax.Actions
{
    public class ErrorActionTest
    {
        [Fact]
        public void ErrorAction_Test()
        {
            var symbols = new SymbolsList(["a"]);
            var action = new ErrorAction();

            Assert.Throws<SyntaxError>(() => action.Perform(new AutomatonToken(symbols.Get(0), 1)));
        }
    }
}
