using Nt.SyntaxParser.Parsing.Structures;
using Nt.SyntaxParser.Syntax.Exceptions;
using Nt.SyntaxParser.Syntax.Structures;
using System.Collections.ObjectModel;

namespace Nt.SyntaxParser.Tests.Syntax
{
    public class SyntaxParserTest
    {
        private static void AssertTokens(TokensList tokens, List<string> reference)
        {
            Assert.Equal(reference.Count, tokens.Count);
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(reference[i], tokens[i].Name);
            }
        }

        private static bool DerivationEquals(Derivation derivation, List<GrammarToken> referenceList)
        {
            if (referenceList.Count != derivation.Count) return false;
            for (int i = 0; i < derivation.Count; i++)
            {
                if (referenceList[i].Index != derivation[i].Index) return false;
                if (referenceList[i].Line != derivation[i].Line) return false;
            }
            return true;
        }

        private static void AssertRules(ICollection<Rule> rules, List<(NonTerminal, List<GrammarToken>)> referenceList)
        {
            Assert.Equal(referenceList.Count, rules.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(rules, rule =>
                    rule.Token != null
                    && rule.Token.Index == reference.Item1.Index
                    && rule.Token.Line == reference.Item1.Line
                    && DerivationEquals(rule.Derivation, reference.Item2));
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            var grammar = parser.ParseString("T = {a, b}\nT={c, d}");

            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
        }

        #endregion

        #region Terminals and NonTerminals

        [Fact]
        public void SyntaxParser_ParseTerminalsAndNonTerminalsTest1()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();

            Assert.Throws<NotDeclaredNonTerminalException>(() => parser.ParseString("S=A"));
        }

        #endregion

        #region Rules

        [Fact]
        public void SyntaxParser_SingleRuleTest()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a}\nR:A -> a;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar.Rules, [(new(0, 3), [new Terminal(0, 3)])]);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_MultipleUniqueRules()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A}\nT={a, b}\nR: A -> a | b;");

            AssertTokens(grammar.NonTerminals, ["A"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar.Rules, [
                (new(0, 3), [new Terminal(0,3)]),
                (new(0, 3), [new Terminal(1,3)])
            ]);
        }

        [Fact]
        public void SyntaxParser_MultipleRulesTest1()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b}\nR:A -> a B;\nR:B -> b;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar.Rules, [
                (new(0, 3), [new Terminal(0,3), new NonTerminal(1,3)]),
                (new(1, 4), [new Terminal(1,4)])
            ]);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_MultipleUniqueRulesTest()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            var grammar = parser.ParseString("N={A,B}\nT={a,b,c,d}\nR:A -> a B | c;\nR:B -> b | d A;");

            AssertTokens(grammar.NonTerminals, ["A", "B"]);
            AssertTokens(grammar.Terminals, ["a", "b", "c", "d"]);
            Assert.Equal(-1, grammar.Axiom);
            AssertRules(grammar.Rules, [
                (new(0, 3), [new Terminal(0,3), new NonTerminal(1,3)]),
                (new(0, 3), [new Terminal(2,3)]),
                (new(1, 4), [new Terminal(1,4)]),
                (new(1, 4), [new Terminal(3,4), new NonTerminal(0,4)])
            ]);
            Assert.Empty(grammar.RegularExpressions);
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest1()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<NotDeclaredNonTerminalException>(() => parser.ParseString("T={a}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest2()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nR:A -> a;"));
        }

        [Fact]
        public void SyntaxParser_RuleUndefinedSymbolTest3()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<UnknownSymbolException>(() => parser.ParseString("N={A}\nT={a}\nR:A -> a B;"));
        }

        #endregion

        #region Regular Expressions

        [Fact]
        public void SyntaxParser_SingleRegexTest()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<NotDeclaredNonTerminalException>(() => parser.ParseString("E:VAR = \"[a-zA-Z_]*\";"));
        }

        #endregion

        #region Miscellaneous

        [Fact]
        public void SyntaxParser_Test1()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
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
            AssertRules(grammar.Rules, [
                (new(0, 4), [new NonTerminal(1,4), new NonTerminal(2,4)]),
                (new(0, 4), [new Terminal(2,4)]),
                (new(1, 5), [new Terminal(0,5), new NonTerminal(1,5)]),
                (new(1, 5), [new NonTerminal(3,5)]),
                (new(2, 6), [new Terminal(1,6), new NonTerminal(2,6)]),
                (new(2, 6), [new NonTerminal(4,6)])
            ]);
            AssertRegex(grammar.RegularExpressions, [
                (new(3, 7), "\"a+\""),
                (new(4, 8), "\"b+\"")
            ]);
        }

        [Fact]
        public void SyntaxParser_Test2()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N=")); // Missing non-terminals declaration
        }

        [Fact]
        public void SyntaxParser_Test3()
        {

            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR: S ->")); // Missing rule derivation
        }

        [Fact]
        public void SyntaxParser_Test4()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE:S =")); // Missing regex pattern
        }

        [Fact]
        public void SyntaxParser_Test5()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nT={a}\nR:S -> a")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_Test6()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<EndOfStringException>(() => parser.ParseString("N={S}\nE: S = a+")); // Missing semicolon
        }

        [Fact]
        public void SyntaxParser_Test7()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("ERROR"));
        }

        [Fact]
        public void SyntaxParser_Test8()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("S +"));
        }

        [Fact]
        public void SyntaxParser_Test9()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("T={a b}"));
        }

        [Fact]
        public void SyntaxParser_Test10()
        {
            var parser = new SyntaxParser.Syntax.SyntaxParser();
            Assert.Throws<SyntaxError>(() => parser.ParseString("N={A}\nT={a}\nR: A -* a"));
        }

        #endregion
    }
}
