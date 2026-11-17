using Nt.Syntax.Exceptions;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax
{
    public class SyntaxParserTest
    {

        #region NonTerminals

        [Fact]
        public void SyntaxParser_SingleNonTerminal_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            AssertTokens(grammar.NonTerminals, ["A"]);
        }

        [Fact]
        public void SyntaxParser_MultipleNonTerminals_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "MultipleNonTerminals");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C"]);
        }

        #endregion

        #region Terminals

        [Fact]
        public void SyntaxParser_SingleTerminal_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            AssertTokens(grammar.Terminals, ["a"]);
        }

        [Fact]
        public void SyntaxParser_MultipleTerminals_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "MultipleTerminals");

            AssertTokens(grammar.Terminals, ["a", "b", "c"]);
        }

        #endregion

        #region Axiom

        [Fact]
        public void SyntaxParser_Axiom_ShouldBeValid()
        {
            var grammar = ParseTest(TestType.Syntax, "Axiom");

            AssertTokens(grammar.NonTerminals, ["S"]);
            AssertAxiom(grammar, "S");
        }

        [Fact]
        public void SyntaxParser_Axiom_ShouldThrowOnUndeclaredSymbol()
        {
            Assert.Throws<AxiomException>(() => ParseTest(TestType.Syntax, "AxiomError"));
        }

        #endregion

        #region Rules

        [Fact]
        public void SyntaxParser_SinglRuleTest_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a"]);
            AssertRules(grammar, [("A", ["a"])]);
        }

        [Fact]
        public void SyntaxParser_MultipleRules_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "MultipleRules");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c"]);
            AssertRules(grammar, [("A", ["a"]), ("B", ["b"]), ("C", ["c"]) ]);
        }

        [Fact]
        public void SyntaxParser_SingleRuleMutlipleDefinitions_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRuleMultipleDefinitions");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [("A", ["a"]), ("A", ["b"]), ("A", ["c"])]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_MultipleRulesMultipleDefinitions_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "MultipleRulesMultipleDefinitions");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c", "d", "e", "f", "g", "h", "i"]);
            AssertRules(grammar, [
                ("A", ["a"]), ("A", ["b"]), ("A", ["c"]),
                ("B", ["d"]), ("B", ["e"]), ("B", ["f"]),
                ("C", ["g"]), ("C", ["h"]), ("C", ["i"])
            ]);
        }

        [Fact]
        public void SyntaxParser_SingleRuleUndefinedAxiom_ShouldThrow()
        {
            Assert.Throws<AxiomException>(() => ParseTest(TestType.Syntax, "SingleRuleUndefinedAxiom"));
        }

        [Fact]
        public void SyntaxParser_SingleRuleUndefinedSymbol_ShouldThrow()
        {
            Assert.Throws<UnknownSymbolException>(() => ParseTest(TestType.Syntax, "SingleRuleUndefinedSymbol"));
        }

        #endregion

        #region Regular Expressions

        [Fact]
        public void SyntaxParser_SingleRegex_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRegex");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertRegex(grammar, [("A", "a")]);
        }

        [Fact]
        public void SyntaxParser_MultipleRegexs_ValidResult()
        {
            var grammar = ParseTest(TestType.Syntax, "MultipleRegexs");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C"]);
            AssertRegex(grammar, [("A", "a"), ("B", "b"), ("C", "c")]);
        }

        [Fact]
        public void SyntaxParser_SingleRegexUndefinedAxiom_ThrowOnUndeclaredAxiom()
        {
            Assert.Throws<AxiomException>(() => ParseTest(TestType.Syntax, "SingleRegexUndefinedAxiom"));
        }

        #endregion

        #region Escape Characters

        [Fact]
        public void SyntaxParser_Escape_UpdateSuccessful()
        {
            var grammar = ParseTest(TestType.PreParsing, "Escape");

            Assert.Equal('$', grammar.EscapeCharacter);
        }

        [Fact]
        public void SyntaxParser_ReadEscape_ReadSuccessful()
        {
            var grammar = ParseTest(TestType.PreParsing, "ReadEscape");

            Assert.Equal('$', grammar.EscapeCharacter);
            AssertTokens(grammar.Terminals, ["a"]);
        }


        #endregion

        #region Importation

        [Fact]
        public void SyntaxParser_SingleImport_ShouldImport()
        {
            var grammar = ParseTest(TestType.Importation, "SingleImport");

            AssertTokens(grammar.Terminals, ["a"]);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertRules(grammar, [("A", ["a"])]);
        }

        [Fact]
        public void SyntaxParser_MultipleImport_ShouldImport()
        {
            var grammar = ParseTest(TestType.Importation, "MultipleImport");

            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertRules(grammar, [("A", ["a"]), ("B", ["b"])]);
        }

        #endregion

        #region Lines

        [Fact]
        public void SyntaxParser_ParseTerminals_ReturnCorrectLine()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleTerminal");

            Assert.Equal(2, grammar.Terminals.Get(0).Line);
        }

        [Fact]
        public void SyntaxParser_ParseNonTerminals_ReturnCorrectLine()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleNonTerminal");

            Assert.Equal(2, grammar.NonTerminals.Get(0).Line);
        }

        [Fact]
        public void SyntaxParser_ParseAxiom_ReturnCorrectLine()
        {
            var grammar = ParseTest(TestType.Syntax, "Axiom");

            Assert.Equal(5, grammar.Axiom?.Line);
        }

        [Fact]
        public void SyntaxParser_ParseRuleAxiom_ReturnCorrectLine()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");

            Assert.Equal(10, grammar.Rules.Get(0).Axiom?.Line);
        }

        [Fact]
        public void SyntaxParser_ParseRuleDerivation_ReturnCorrectLine()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRule");

            Assert.Equal(10, grammar.Rules.Get(0).Derivation.Get(0).Line);
        }

        [Fact]
        public void SyntaxParser_ParseRegexAxiom_ReturnCorrectLine()
        {
            var grammar = ParseTest(TestType.Syntax, "SingleRegex");
            Assert.Equal(6, grammar.RegularExpressions.Get(0).Axiom?.Line);
        }

        #endregion

    }
}
