using Nt.Syntax.Payload;
using Nt.Syntax.Payload.Exceptions;
using Nt.Syntax.Structures;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax.Payload
{

    internal class PayloadData(string message) : IPayload
    {
        public string Message { get; } = message;
    }

    internal class WrongPayloadData() : IPayload { }

    public class PayloadExtensionsTest
    {

        // Test payload type

        [Fact] 
        public void PayloadExtensions_SetPayload_OnTerminals_ShouldSetPayload()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            var terminal = grammar.Terminals[0];
            terminal.SetPayload(new PayloadData(""));

            var data = terminal.GetPayload<PayloadData>();
            Assert.IsType<PayloadData>(data);
        }

        [Fact]
        public void PayloadExtensions_SetPayload_OnNonTerminals_ShouldSetPayload()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            var nonTerminal = grammar.NonTerminals[0];
            nonTerminal.SetPayload(new PayloadData(""));

            var data = nonTerminal.GetPayload<PayloadData>();
            Assert.IsType<PayloadData>(data);
        }

        [Fact]
        public void PayloadExtensions_SetPayload_OnRules_ShouldSetPayload()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");
            
            var rule = grammar.Rules[0];
            rule.SetPayload(new PayloadData(""));

            var data = rule.GetPayload<PayloadData>();
            Assert.IsType<PayloadData>(data);
        }

        [Fact]
        public void PayloadExtensions_SetPayload_OnRegularExpressions_ShouldSetPayload()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRegex");

            var regex = grammar.RegularExpressions[0];
            regex.SetPayload(new PayloadData(""));

            var data = regex.GetPayload<PayloadData>();
            Assert.IsType<PayloadData>(data);
        }


        // Retrieve message set in payload

        [Fact]
        public void PayloadExtensions_GetPayload_OnTerminals_ShouldReturnMessage()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            var terminal = grammar.Terminals[0];
            terminal.SetPayload(new PayloadData("Hello World!"));

            var data = terminal.GetPayload<PayloadData>();
            Assert.Equal("Hello World!", data.Message);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnNonTerminals_ShouldReturnMessage()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            var nonTerminal = grammar.NonTerminals[0];
            nonTerminal.SetPayload(new PayloadData("Hello World!"));

            var data = nonTerminal.GetPayload<PayloadData>();
            Assert.Equal("Hello World!", data.Message);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnRules_ShouldReturnMessage()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");
            var rule = grammar.Rules[0];
            rule.SetPayload(new PayloadData("Hello World!"));
            var data = rule.GetPayload<PayloadData>();
            Assert.Equal("Hello World!", data.Message);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnRegularExpressions_ShouldReturnMessage()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRegex");

            var regex = grammar.RegularExpressions[0];
            regex.SetPayload(new PayloadData("Hello World!"));

            var data = regex.GetPayload<PayloadData>();
            Assert.Equal("Hello World!", data.Message);
        }


        // Throw if payload is not set

        [Fact]
        public void PayloadExtensions_GetPayload_OnTerminals_ShouldThrowIfNotSet()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            var terminal = grammar.Terminals[0];

            Assert.Throws<NullPayloadException>(terminal.GetPayload<PayloadData>);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnNonTerminals_ShouldThrowIfNotSet()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            var nonTerminal = grammar.NonTerminals[0];

            Assert.Throws<NullPayloadException>(nonTerminal.GetPayload<PayloadData>);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnRules_ShouldThrowIfNotSet()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");

            var rule = grammar.Rules[0];

            Assert.Throws<NullPayloadException>(rule.GetPayload<PayloadData>);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnRegularExpressions_ShouldThrowIfNotSet()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRegex");

            var regex = grammar.RegularExpressions[0];

            Assert.Throws<NullPayloadException>(regex.GetPayload<PayloadData>);
        }


        // Throw if payload is of wrong type

        [Fact]
        public void PayloadExtensions_GetPayload_OnTerminals_ShouldThrowIfWrongType()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            var terminal = grammar.Terminals[0];
            terminal.SetPayload(new PayloadData(""));

            Assert.Throws<PayloadTypeException>(terminal.GetPayload<WrongPayloadData>);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnNonTerminals_ShouldThrowIfWrongType()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            var nonTerminal = grammar.NonTerminals[0];
            nonTerminal.SetPayload(new PayloadData(""));

            Assert.Throws<PayloadTypeException>(nonTerminal.GetPayload<WrongPayloadData>);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnRules_ShouldThrowIfWrongType()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");

            var rule = grammar.Rules[0];
            rule.SetPayload(new PayloadData(""));

            Assert.Throws<PayloadTypeException>(rule.GetPayload<WrongPayloadData>);
        }

        [Fact]
        public void PayloadExtensions_GetPayload_OnRegularExpressions_ShouldThrowIfWrongType()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRegex");

            var regex = grammar.RegularExpressions[0];
            regex.SetPayload(new PayloadData(""));

            Assert.Throws<PayloadTypeException>(regex.GetPayload<WrongPayloadData>);
        }




    }
}
