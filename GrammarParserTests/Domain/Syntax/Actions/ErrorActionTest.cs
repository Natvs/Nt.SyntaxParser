using GrammarParser.Domain.Parsing.Structures;
using GrammarParser.Domain.Syntax.Actions;
using GrammarParser.Domain.Syntax.Exceptions;
using GrammarParser.Domain.Syntax.Structures;

namespace Tests.Domain.Syntax.Actions
{
    public class ErrorActionTest
    {
        [Fact]
        public void ErrorAction_Test()
        {
            var tokens = new TokensList(["a"]);
            var action = new ErrorAction(tokens);

            Assert.Throws<SyntaxError>(() => action.Perform(new ParsedToken(0, 1)));
        }
    }

    public class SetAxiomActionTest
    {
        [Fact]
        public void SetAxiomAction_Test1()
        {
            var tokens = new TokensList(["A", "B", "C"]);
            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["A", "B", "C"]);

            var action = new SetAxiomAction(grammar, tokens);
            action.Perform(new(2, 1));

            Assert.Equal("C", grammar.NonTerminals[grammar.Axiom].Name);
        }

        [Fact]
        public void SetAxiomAction_Test2()
        {
            var tokens = new TokensList(["A", "B", "C"]);
            var grammar = new Grammar();
            grammar.NonTerminals.AddRange(["A", "B"]);

            var action = new SetAxiomAction(grammar, tokens);
            Assert.Throws<NotDeclaredNonTerminalException>(() => action.Perform(new(2, 1)));
        }
    }
}
