using Xunit;

namespace GrammarParserTest.Domain.Parser
{
    public class ParserTest()
    {
        [Fact]
        public void ParseSeparator_Test1()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([' '], []);
            var stringToParse  = "a b c d e";
            var expectedTokens = new List<string>(["a", "b", "c", "d", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSeparator_Test2()
        {
            var parser = new GrammarParser.Domain.Parser.Parser(['-'], []);
            var stringToParse  = "a-b--c---d----e";
            var expectedTokens = new List<string>(["a", "b", "c", "d", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test1()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([' '], ["+", "-", "*", "/"]);
            var stringToParse  = "a + b - c * d / e";
            var expectedTokens = new List<string>(["a", "+", "b", "-", "c", "*", "d", "/", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test2()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([], ["+", "-", "*", "/"]);
            var stringToParse = "a+b-c*d/e";
            var expectedTokens = new List<string>(["a", "+", "b", "-", "c", "*", "d", "/", "e"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseSymbols_Test3()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([' '], ["+", "++", "+++"]);
            var stringToParse  = "a+ b++ c+++";
            var expectedTokens = new List<string>(["a", "+", "b", "++", "c", "+++"]);
            ParseString(parser, stringToParse, expectedTokens);      
        }


        [Fact]
        public void ParseSymbols_Test4()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([], ["+", "++", "+++"]);
            var stringToParse  = "a+b++c+++";
            var expectedTokens = new List<string>(["a", "+", "b", "++", "c", "+++"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test1()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([], []);
            var stringToParse = "\\a";
            var expectedTokens = new List<string>(["\\a"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test2()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([], []);
            var stringToParse = "a\\b";
            var expectedTokens = new List<string>(["a\\b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test3()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([], []);
            var stringToParse = "\\ab";
            var expectedTokens = new List<string>(["\\ab"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        [Fact]
        public void ParseEscape_Test4()
        {
            var parser = new GrammarParser.Domain.Parser.Parser([], []);
            var stringToParse = "a\\\\b";
            var expectedTokens = new List<string>(["a\\\\b"]);
            ParseString(parser, stringToParse, expectedTokens);
        }

        private static void ParseString(GrammarParser.Domain.Parser.Parser parser, string stringToParse, List<string> expectedTokens)
        {
            var result = parser.Parse(stringToParse);

            Assert.Equal(expectedTokens.Count, result.Parsed.Count);
            for (var i = 0; i < expectedTokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i], result.Tokens[result.Parsed[i].TokenIndex].Name);
            }
        }

    }
}
