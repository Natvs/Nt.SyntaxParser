using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using static Nt.Tests.Syntax.SyntaxTestUtils;

namespace Nt.Tests.Syntax
{
    public class SyntaxParserTest
    {
        #region NonTerminals

        [Fact]
        public void SyntaxParser_ParseNonTerminalTest()
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
        public void SyntaxParser_ParseNonTerminalsTest1()
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
        public void SyntaxParser_ParseNonTerminalsTest2()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N = {A, B}\nN={C, D}");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            AssertTokens(grammar.Terminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        #endregion

        #region Terminals

        [Fact]
        public void SyntaxParser_ParseTerminalTest()
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
        public void SyntaxParser_ParseTerminalsTest1()
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
        public void SyntaxParser_ParseTerminalsTest2()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {a, b}\nT={c, d}");

            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_ParseSymbolTest1()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {=}");

            AssertTokens(grammar.Terminals, ["="]);
            AssertTokens(grammar.NonTerminals, []);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_ParseSymbolTest2()
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
        public void SyntaxParser_ParseSymbolTest3()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {===}");
            AssertTokens(grammar.NonTerminals, []);
            AssertTokens(grammar.Terminals, ["==="]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, []);
            AssertRegex(grammar, []);
        }

        #endregion

        #region Terminals and NonTerminals

        [Fact]
        public void SyntaxParser_ParseTerminalsAndNonTerminalsTest1()
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
        public void SyntaxParser_ParseTerminalsAndNonTerminalsTest2()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {a, b}\nN = {A, B}\nT={c, d}\nN={C, D}");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
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
        public void SyntaxParser_SingleRuleTest()
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
        public void SyntaxParser_MultipleUniqueRules()
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
        public void SyntaxParser_MultipleRulesTest1()
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
        public void SyntaxParser_MultipleUniqueRulesTest()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b,c,d}\nR:A -> a B | c;\nR:B -> b | d A;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            AssertAxiom(grammar, "");
            AssertRules(grammar, [
                ("A", ["a", "B"]),
                ("A", ["c"]),
                ("B", ["b"]),
                ("B", ["d", "A"])
            ]);
            AssertRegex(grammar, []);
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest1()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnregisteredNonTerminalException>(() => parser.ParseString("T={a}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest2()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest3()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nT={a}\nR:A -> a B;"));
        }

        #endregion

        #region Regular Expressions

        [Fact]
        public void SyntaxParser_SingleRegexTest()
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
        public void SyntaxParser_MultipleRegexTest()
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
        public void SyntaxParser_RegexUndefinedSymbolTest()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<UnregisteredNonTerminalException>(() => parser.ParseString("E:VAR = \"[a-zA-Z_]*\";"));
        }

        #endregion

        #region Escape Characters

        [Fact]
        public void SyntaxParser_EscapeCharacterTest1()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $");

            Assert.Equal('$', grammar.EscapeCharacter);
        }

        [Fact]
        public void SyntaxParser_EscapeCharacterTest2()
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
        public void SyntaxParser_EscapeCharacterTest3()
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
        public void SyntaxParser_EscapeCharacterTest4()
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
        public void SyntaxParser_EscapeCharacterTest5()
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
        public void SyntaxParser_EscapeCharacterTest6()
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
        public void SyntaxParser_Test1()
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

        [Fact]
        public void SyntaxParser_Test2()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N=")); // Missing non-terminals declaration
        }

        [Fact]
        public void SyntaxParser_Test3()
        {

            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR: S ->")); // Missing rule derivation
        }

        [Fact]
        public void SyntaxParser_Test4()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE:S =")); // Missing regex pattern
        }

        [Fact]
        public void SyntaxParser_Test5()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR:S -> a")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_Test6()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE: S = a+")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_Test7()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("ERROR"));
        }

        [Fact]
        public void SyntaxParser_Test8()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("S +"));
        }

        [Fact]
        public void SyntaxParser_Test9()
        {
            var parser = new Nt.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("N={A}\nT={a}\nR: A -* a"));
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
