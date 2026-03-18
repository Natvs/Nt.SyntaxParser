using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax
{
    public class SyntaxParserTest
    {
        #region NonTerminals

        [Fact]
        public void SyntaxParser_OldStyleSingleNonTerminal_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N = {A}");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_OldStyleMultipleNonTerminals_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N = {A, B, C, D}");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            AssertTokens(grammar.Terminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }


        [Fact]
        public void SyntaxParser_NewStyleSingleNonTerminal_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("NONTERMINALS: A;");
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_NewStyleMultipleNonTerminal_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("NONTERMINALS: A, B, C, D;");
            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            AssertTokens(grammar.Terminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        #endregion

        #region Terminals

        [Fact]
        public void SyntaxParser_OldStyleSingleTerminal_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {a}");

            AssertTokens(grammar.Terminals, ["a"]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_OldStyleMultipleTerminals_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {a, b, c, d}");

            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_NewStyleSingleTerminal_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("TERMINALS: a;");

            AssertTokens(grammar.Terminals, ["a"]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_NewStyleMultipleTerminals_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("TERMINALS: a, b, c, d;");

            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_OldStyleSimpleSymbol_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {=}");
            AssertTokens(grammar.NonTerminals, []);
            AssertTokens(grammar.Terminals, ["="]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_OldStyleDoubleSymbol_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {==}");
            AssertTokens(grammar.NonTerminals, []);
            AssertTokens(grammar.Terminals, ["=="]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_NewStyleSimpleSymbol_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("TERMINALS: =;");
            AssertTokens(grammar.NonTerminals, []);
            AssertTokens(grammar.Terminals, ["="]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_NewStyleDoubleSymbol_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("TERMINALS: ==;");
            AssertTokens(grammar.NonTerminals, []);
            AssertTokens(grammar.Terminals, ["=="]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        #endregion

        #region Terminals and NonTerminals

        [Fact]
        public void SyntaxParser_OldStyleTerminalsAndNonTerminals_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N = {A, B}\nT = {a, b}");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_NewStyleTerminalsAndNonTerminals_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("NONTERMINALS: A, B;\nTERMINALS: a, b;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        #endregion

        #region Axiom

        [Fact]
        public void SyntaxParser_AxiomTest1()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nS=A");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertAxiom(grammar, "A");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_AxiomTest2()
        {
            var parser = new Nt.Syntax.SyntaxParser();

            Assert.Throws<UnregisteredNonTerminalException>(() => parser.ParseString("S=A"));
        }

        #endregion

        #region Rules

        [Fact]
        public void SyntaxParser_SingleOldStyleRuleRule_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a}\nR:A -> a;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [("A", ["a"])]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_SingleNewStyleRuleTest_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a}\nRULES: A -> a;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [("A", ["a"])]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_MultipleUniqueOldStyleRules_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a, b}\nR: A -> a | b;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [("A", ["a"]), ("A", ["b"]) ]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_MultipleUniqueNewStyleRules_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a,b}\nRULES: A -> a | b;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [("A", ["a"]), ("A", ["b"])]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_MultipleOldStyleRules_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b}\nR:A -> a B;\nR:B -> b;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [
                ("A", ["a", "B"]),
                ("B", ["b"])
            ]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_MultipleNewStyleRules_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b}\nRULES:\nA -> a B,\nB -> b;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [
                ("A", ["a", "B"]),
                ("B", ["b"])
            ]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_Rule_ThrowOnUndeclaredSymbol()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnregisteredNonTerminalException>(() => parser.ParseString("T={a}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_Rule_ThrowOnUndeclaredTerminal()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_Rule_ThrowOnUndeclaredNonTerminal()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nT={a}\nR:A -> a B;"));
        }

        #endregion

        #region Regular Expressions

        [Fact]
        public void SyntaxParser_SingleOldStyleRegex_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={VAR}\nE:VAR = \"[a-zA-Z_]*\";");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["VAR"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, [("VAR", "\"[a-zA-Z_]*\"")]);
        }

        [Fact]
        public void SyntaxParser_SingleNewStyleRegex_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={VAR}\nREGULAR EXPRESSIONS: VAR = \"[a-zA-Z_]*\";");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["VAR"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, [("VAR", "\"[a-zA-Z_]*\"")]);
        }

        [Fact]
        public void SyntaxParser_MultipleOldStyleRegex_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={VAR, NUM}\nE:VAR = \"[a-zA-Z_]*\";\nE:NUM = \"[0-9]+\";");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["VAR", "NUM"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, [
                ("VAR", "\"[a-zA-Z_]*\""),
                ("NUM", "\"[0-9]+\"")
            ]);
        }

        [Fact]
        public void SyntaxParser_MultipleNewStyleRegex_ValidResult()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={VAR, NUM}\nREGULAR EXPRESSIONS: VAR = \"[a-zA-Z_]*\",\nNUM = \"[0-9]+\";");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["VAR", "NUM"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, [
                ("VAR", "\"[a-zA-Z_]*\""),
                ("NUM", "\"[0-9]+\"")
            ]);
        }

        [Fact]
        public void SyntaxParser_Regex_ThrowOnUndeclaredSymbol()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnregisteredNonTerminalException>(() => parser.ParseString("E:VAR = \"[a-zA-Z_]*\";"));
        }

        #endregion

        #region Escape Characters

        [Fact]
        public void SyntaxParser_UpdateEscapeCharacter_UpdateSuccessful()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $");

            Assert.Equal('$', grammar.EscapeCharacter);
        }

        [Fact]
        public void SyntaxParser_UpdateEscapeCharacter_ReadSuccessfulForTerminal()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nT={$a}");

            AssertTokens(grammar.Terminals, ["a"]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_UpdateEscapeCharacter_ReadSuccessfulForNonTerminal()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nN={$A}");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_UpdateEscapeTerminal_ReadSuccessfulForAxiom()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nN={A}\nS=$A");
            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertAxiom(grammar, "A");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_UpdateEscapeCharacter_ReadSuccessfulForRule()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nT={a}\nN={A}\nR:A->$a;");

            AssertTokens(grammar.Terminals, ["a"]);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [("A", ["a"])]); // Pre-parsing instructions do not count as a new line
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_UpdateEscapeCharacter_ReadSuccessfulForRegex()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nN={A}\nE:A=$a;");

            AssertTokens(grammar.Terminals, []);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, [("A", "a")]);
        }

        [Fact]
        public void SyntaxParser_ParsingOldStyle_ReadSuccessful()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={S,A,B,C,D}" +
                "\nT={a,b,c}" +
                "\nS=S" +
                "\nR:S -> A B | c;" +
                "\nR:A -> a A | C;" +
                "\nR:B -> b B | D;" +
                "\nE:C = \"a+\";" +
                "\nE:D = \"b+\";"
            );

            AssertTokens(grammar.NonTerminals, ["S", "A", "B", "C", "D"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c"]);
            AssertAxiom(grammar, "S");
            AssertRules(grammar, [
                ("S", ["A", "B"]),
                ("S", ["c"]),
                ("A", ["a", "A"]),
                ("A", ["C"]),
                ("B", ["b", "B"]),
                ("B", ["D"])
            ]);
            AssertRegex(grammar, [
                ("C", "\"a+\""),
                ("D", "\"b+\"")
            ]);
        }

        #endregion

        #region Incomplete Parsing

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnMissingNonTerminalsDeclaration()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N=")); // Missing non-terminals declaration
        }

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnMissingTerminalsDeclaration()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("T=")); // Missing terminals declaration
        }

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnMissingRuleDerivation()
        {

            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR: S ->")); // Missing rule derivation
        }

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnMissingRegularExpressionDeclaration()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE:S =")); // Missing regex pattern
        }

        [Fact]
        public void SyntaxParser_IncompletParsing_ThrowOnMissingSemiColumnAfterRule()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR:S -> a")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_IncompletParsing_ThrowOnMissingSemiColumnAfterRegex()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE: S = a+")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnUnknowSymbol()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("ERROR"));
        }

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnAxiomInvalidSymbol()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("S +"));
        }

        [Fact]
        public void SyntaxParser_IncompletParsing_ThrowOnRuleSymbol1()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("N={A}\nT={a}\nR: A *-> a;"));
        }

        [Fact]
        public void SyntaxParser_IncompleParsing_ThrowOnRuleSymbol2()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("N={A}\nT={a}\nR: A --* a;"));
        }

        [Fact]
        public void SyntaxParser_IncompleteParsing_ThrowOnRegexInvalidSymbol()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("N={INT}\nE: INT + [0-9];"));
        }

        #endregion

        #region Importation

        [Fact]
        public void SyntaxParser_RecursiveSingleImportTest()
        {
            var parser = new Nt.Syntax.SyntaxParser();

            var filename = "../../../Resources/Importation/single_import_grammar.txt";
            Grammar grammar = parser.ParseFile(filename);

            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertAxiom(grammar, "A");
            AssertRules(grammar, [("A", ["a"]), ("A", ["b"])]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_RecursiveMultipleImportTest()
        {
            var parser = new Nt.Syntax.SyntaxParser();

            var filename = "../../../Resources/Importation/multiple_import_grammar.txt";
            Grammar grammar = parser.ParseFile(filename);

            AssertTokens(grammar.Terminals, ["a", "b"]);
            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertAxiom(grammar, "A");
            AssertRules(grammar, [("A", ["a"]), ("B", ["b"]), ("A", ["B"])]);
            AssertRegex(grammar, []);
        }

        #endregion

    }
}
