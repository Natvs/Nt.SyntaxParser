using Nt.Syntax.Actions;
using Nt.Syntax.Exceptions;
using Nt.Parser.Structures;

namespace Nt.Tests.Domain.Syntax.Actions
{
    public class ErrorActionTest
    {
        [Fact]
        public void ErrorAction_Test()
        {
            var symbols = new SymbolsList(["a"]);
            var action = new ErrorAction();

            Assert.Throws<SyntaxError>(() => action.Perform(new ParsedToken(symbols.Get(0), 1)));
        }
    }
}
