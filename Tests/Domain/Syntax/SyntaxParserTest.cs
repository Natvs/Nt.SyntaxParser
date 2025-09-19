using GrammarParser.Parsing.Structures;
using GrammarParser.Syntax;
using GrammarParser.Syntax.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Domain.Syntax
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

        private static void AssertRules(List<Rule> rules, List<(NonTerminal, List<GrammarToken>)> referenceList)
        {
            Assert.Equal(referenceList.Count, rules.Count);
            for (int i = 0; i < rules.Count; i++)
            {
                var rule = rules[i];
                var reference = referenceList[i];
                Assert.NotNull(rule.Token);
                Assert.Equal(reference.Item1.Index, rule.Token.Index);
                Assert.Equal(reference.Item1.Line, rule.Token.Line);

                Assert.Equal(reference.Item2.Count, rule.Derivation.Count);
                for (int j = 0; j < rule.Derivation.Count; j++) 
                {
                    Assert.Equal(reference.Item2[j].Index, rule.Derivation[j].Index);
                    Assert.Equal(reference.Item2[j].Line, rule.Derivation[j].Line);
                }
            }
        }

        private static void AssertRegex(List<RegularExpression> regexList, List<(NonTerminal, string)> referenceList)
        {
            Assert.Equal(referenceList.Count, regexList.Count);
            for (int i = 0; i < regexList.Count; i++)
            {
                var regex = regexList[i];
                var reference = referenceList[i];

                Assert.NotNull(regex.Token);
                Assert.Equal(reference.Item1.Index, regex.Token.Index);
                Assert.Equal(reference.Item1.Line, regex.Token.Line);
                Assert.Equal(reference.Item2, regex.Pattern);
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
            var grammar = parser.ParseString("S=A");

            Assert.Empty(grammar.Terminals);
            Assert.Empty(grammar.NonTerminals);
            Assert.Equal(-1, grammar.Axiom);
            Assert.Empty(grammar.Rules);
            Assert.Empty(grammar.RegularExpressions);
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
            AssertRules(grammar.Rules, [ (new(0, 3), [new Terminal(0,3)]) ]);
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
            AssertRules(grammar.Rules, [
                (new(0, 3), [new Terminal(0,3)]),
                (new(0, 3), [new Terminal(1,3)])
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
            AssertRules(grammar.Rules, [
                (new(0, 3), [new Terminal(0,3), new NonTerminal(1,3)]),
                (new(1, 4), [new Terminal(1,4)])
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
            AssertRules(grammar.Rules, [
                (new(0, 3), [new Terminal(0,3), new NonTerminal(1,3)]),
                (new(0, 3), [new Terminal(2,3)]),
                (new(1, 4), [new Terminal(1,4)]),
                (new(1, 4), [new Terminal(3,4), new NonTerminal(0,4)])
            ]);
            Assert.Empty(grammar.RegularExpressions);
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

        #endregion

        #region Miscellaneous

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

        #endregion
    }
}
