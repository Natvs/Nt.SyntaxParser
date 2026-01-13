using Nt.Parsing.Structures;
using Nt.Syntax.Exceptions;
using Nt.Syntax.Structures;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit.Sdk;

namespace Nt.Syntax.Tests
{
    public class SyntaxParserTest
    {
        private static void AssertTokens(SymbolsList tokens, List<string> reference)
        {
            Assert.Equal(reference.Count, tokens.Count);
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(reference[i], tokens[i].Name);
            }
        }

        private static bool DerivationEquals(Grammar grammar, Derivation derivation, List<(string, int)> referenceList)
        {
            if (referenceList.Count != derivation.Count) return false;
            for (int i = 0; i < derivation.Count; i++)
            {
                var symbol_index = -1;
                if (grammar.Terminals.Contains(referenceList[i].Item1)) symbol_index = grammar.Terminals.IndexOf(referenceList[i].Item1);
                else symbol_index = grammar.NonTerminals.IndexOf(referenceList[i].Item1);
                if (symbol_index != derivation[i].Index) return false;
                if (referenceList[i].Item2 != derivation[i].Line) return false;
            }
            return true;
        }

        private static void AssertRules(Grammar grammar, List<(string, int, List<(string, int)>)> referenceList)
        {
            Assert.Equal(referenceList.Count, grammar.Rules.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(grammar.Rules, rule =>
                    rule.Token != null
                    && rule.Token.Index == grammar.NonTerminals.IndexOf(reference.Item1)
                    && rule.Token.Line == reference.Item2
                    && DerivationEquals(grammar, rule.Derivation, reference.Item3));
            }
        }

        private static void AssertRegex(ICollection<RegularExpression> regexList, List<(NonTerminal, string)> referenceList)
        {
            Assert.Equal(referenceList.Count, regexList.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(regexList, regex => 
                    regex.Token != null
                    && regex.Token.Index == reference.Item1.Index
                    && regex.Token.Line == reference.Item1.Line
                    && regex.Pattern == reference.Item2);
            }
        }

        #region NonTerminals

        [Fact]
        public void SyntaxParser_ParseNonTerminalTest()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {A}");

            AssertTokens(grammar.NonTerminals, ["A"]);
            Assert.Empty(grammar.Terminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseNonTerminalsTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {A, B, C, D}");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            Assert.Empty(grammar.Terminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseNonTerminalsTest2()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {A, B}\nN={C, D}");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            Assert.Empty(grammar.Terminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        #endregion

        #region Terminals

        [Fact]
        public void SyntaxParser_ParseTerminalTest()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("T = {a}");

            AssertTokens(grammar.Terminals, ["a"]);

            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseTerminalsTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("T = {a, b, c, d}");

            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseTerminalsTest2()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("T = {a, b}\nT={c, d}");

            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseSymbolTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("T = {=}");

            AssertTokens(grammar.Terminals, ["="]);
            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseSymbolTest2()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {==}");
            AssertTokens(grammar.NonTerminals, ["=="]);
            Assert.Empty(grammar.Terminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseSymbolTest3()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {===}");
            AssertTokens(grammar.NonTerminals, ["==="]);
            Assert.Empty(grammar.Terminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        #endregion

        #region Terminals and NonTerminals

        [Fact]
        public void SyntaxParser_ParseTerminalsAndNonTerminalsTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N = {A, B}\nT = {a, b}");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_ParseTerminalsAndNonTerminalsTest2()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("T = {a, b}\nN = {A, B}\nT={c, d}\nN={C, D}");

            AssertTokens(grammar.NonTerminals, ["A", "B", "C", "D"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        #endregion

        #region Axiom

        [Fact]
        public void SyntaxParser_AxiomTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={A}\nS=A");

            Assert.Empty(grammar.Terminals);
            AssertTokens(grammar.NonTerminals, ["A"]);
            Assert.Equal(0, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_AxiomTest2()
        {
            var parser = new SyntaxParser();

            Assert.Throws<NotDeclaredNonTerminalException>(() => parser.ParseString("S=A"));
        }

        #endregion

        #region Rules

        [Fact]
        public void SyntaxParser_SingleRuleTest()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a}\nR:A -> a;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar, [("A", 3, [("a", 3)])]);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_MultipleUniqueRules()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a, b}\nR: A -> a | b;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar, [
                ("A", 3, [("a", 3)]),
                ("A", 3, [("b", 3)])
            ]);
        }

        [Fact]
        public void SyntaxParser_MultipleRulesTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b}\nR:A -> a B;\nR:B -> b;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar, [
                ("A", 3, [("a",3), ("B",3)]),
                ("B", 4, [("b", 4)])
            ]);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_MultipleUniqueRulesTest()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b,c,d}\nR:A -> a B | c;\nR:B -> b | d A;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar, [
                ("A", 3, [("a", 3), ("B", 3)]),
                ("A", 3, [("c", 3)]),
                ("B", 4, [("b", 4)]),
                ("B", 4, [("d", 4), ("A", 4)])
            ]);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest1()
        {
            var parser = new SyntaxParser();
            Assert.Throws<NotDeclaredNonTerminalException>(() => parser.ParseString("T={a}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest2()
        {
            var parser = new SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest3()
        {
            var parser = new SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nT={a}\nR:A -> a B;"));
        }

        #endregion

        #region Regular Expressions

        [Fact]
        public void SyntaxParser_SingleRegexTest()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={VAR}\nE:VAR = \"[a-zA-Z_]*\";");

            Assert.Empty(grammar.Terminals);
            AssertTokens(grammar.NonTerminals, ["VAR"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            AssertRegex(grammar.RegularExpressions, [(new(0, 2), "\"[a-zA-Z_]*\"")]);
        }

        [Fact]
        public void SyntaxParser_MultipleRegexTest()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("N={VAR, NUM}\nE:VAR = \"[a-zA-Z_]*\";\nE:NUM = \"[0-9]+\";");

            Assert.Empty(grammar.Terminals);
            AssertTokens(grammar.NonTerminals, ["VAR", "NUM"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            AssertRegex(grammar.RegularExpressions, [
                (new(0, 2), "\"[a-zA-Z_]*\""),
                (new(1, 3), "\"[0-9]+\"")
            ]);
        }

        [Fact]
        public void SyntaxParser_RegexUndefinedSymbolTest()
        {
            var parser = new SyntaxParser();
            Assert.Throws<NotDeclaredNonTerminalException>(() => parser.ParseString("E:VAR = \"[a-zA-Z_]*\";"));
        }

        #endregion

        #region Escape Characters

        [Fact]
        public void SyntaxParser_EscapeCharacterTest1()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nT={$a}");

            Assert.Single(grammar.Terminals);
            AssertTokens(grammar.Terminals, ["a"]);
            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_EscapeCharacterTest2()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nN={$A}");

            Assert.Empty(grammar.Terminals);
            Assert.Single(grammar.NonTerminals);
            AssertTokens(grammar.NonTerminals, ["A"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_EscapeCharacterTest3()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nN={A}\nS=$A");
            Assert.Empty(grammar.Terminals);
            Assert.Single(grammar.NonTerminals);
            AssertTokens(grammar.NonTerminals, ["A"]);
            Assert.Equal(0, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_EscapeCharacterTest4()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nT={a}\nN={A}\nR:A->$a;");

            Assert.Single(grammar.Terminals);
            AssertTokens(grammar.Terminals, ["a"]);
            Assert.Single(grammar.NonTerminals);
            AssertTokens(grammar.NonTerminals, ["A"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Single(grammar.Rules);
            AssertRules(grammar, [( "A", 3, [("a", 3)] )]); // Pre-parsing instructions do not count as a new line
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_EscapeCharacterTest5()
        {
            var parser = new SyntaxParser();
            var grammar = parser.ParseString("ESCAPE $\nN={A}\nE:A=$a;");

            Assert.Empty(grammar.Terminals);
            Assert.Single(grammar.NonTerminals);
            AssertTokens(grammar.NonTerminals, ["A"]);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Single(grammar.RegularExpressions);
            AssertRegex(grammar.RegularExpressions, [(new(0, 2), "a")]);
        }

        [Fact]
        public void SyntaxParser_Test1()
        {
            var parser = new SyntaxParser();
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
            Assert.Equal(0, grammar.Axiom);
            AssertRules(grammar, [
                ("S", 4, [("A",4), ("B",4)]),
                ("S", 4, [("c",4)]),
                ("A", 5, [("a",5), ("A",5)]),
                ("A", 5, [("C",5)]),
                ("B", 6, [("b",6), ("B",6)]),
                ("B", 6, [("D",6)])
            ]);
            AssertRegex(grammar.RegularExpressions, [
                (new(3, 7), "\"a+\""),
                (new(4, 8), "\"b+\"")
            ]);
        }

        [Fact]
        public void SyntaxParser_Test2()
        {
            var parser = new SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N=")); // Missing non-terminals declaration
        }

        [Fact]
        public void SyntaxParser_Test3()
        {

            var parser = new SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR: S ->")); // Missing rule derivation
        }

        [Fact]
        public void SyntaxParser_Test4()
        {
            var parser = new SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE:S =")); // Missing regex pattern
        }

        [Fact]
        public void SyntaxParser_Test5()
        {
            var parser = new SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR:S -> a")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_Test6()
        {
            var parser = new SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE: S = a+")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_Test7()
        {
            var parser = new SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("ERROR"));
        }

        [Fact]
        public void SyntaxParser_Test8()
        {
            var parser = new SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("S +"));
        }

        [Fact]
        public void SyntaxParser_Test9()
        {
            var parser = new SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("N={A}\nT={a}\nR: A -* a"));
        }

        #endregion
    }
}
