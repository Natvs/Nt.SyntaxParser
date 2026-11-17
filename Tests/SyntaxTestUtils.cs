using Nt.Parser.Structures;
using Nt.Syntax.Structures;

namespace Nt.Tests.Syntax
{
    internal class SyntaxTestUtils
    {
        public enum TestType
        {
            Syntax,
            Importation,
            PreParsing
        }


        public static string GetTestFile(TestType type, string file)
        {
            return type switch
            {
                TestType.Syntax => "../../../Resources/Syntax/" + file + ".txt",
                TestType.Importation => "../../../Resources/Importation/" + file + ".txt",
                TestType.PreParsing => "../../../Resources/PreParsing/" + file + ".txt",
                _ => "no file",
            };
        }

        public static Grammar ParseTest(TestType type, string file)
        {
            var parser = new Nt.Syntax.SyntaxParser();
            if (!File.Exists(GetTestFile(type, file)))
            {
                throw new FileNotFoundException($"Test file {file} not found.");
            }
            var grammar = parser.ParseString(File.ReadAllText(GetTestFile(type, file)));
            return grammar;
        }

        #region Grammar checking
        internal static void AssertTokens(ElementsSet<Terminal> tokens, List<string> reference)
        {
            Assert.Equal(reference.Count, tokens.Count);
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(reference[i], tokens.Get(i).Name);
            }
        }
        internal static void AssertTokens(ElementsSet<NonTerminal> tokens, List<string> reference)
        {
            Assert.Equal(reference.Count, tokens.Count);
            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(reference[i], tokens.Get(i).Name);
            }
        }

        internal static void AssertRules(Grammar grammar, List<(string, List<string>)> referenceList)
        {
            Assert.Equal(referenceList.Count, grammar.Rules.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(grammar.Rules, rule =>
                    rule.Axiom != null
                    && rule.Axiom.Name.Equals(reference.Item1)
                    && DerivationEquals(grammar, rule.Derivation, reference.Item2));
            }
        }

        internal static void AssertRegex(Grammar grammar, List<(string, string)> referenceList)
        {
            Assert.Equal(referenceList.Count, grammar.RegularExpressions.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(grammar.RegularExpressions, regex =>
                    regex.Axiom != null
                    && regex.Axiom.Name == reference.Item1
                    && regex.Pattern == reference.Item2);
            }
        }

        internal static void AssertAxiom(Grammar grammar, string axiom)
        {
            if (axiom.Equals(""))
            {
                Assert.Null(grammar.Axiom);
                return;
            }
            Assert.True(grammar.NonTerminals.Contains((t) => t.Name == axiom));
            Assert.NotNull(grammar.Axiom);
            Assert.Equal(axiom, grammar.Axiom.Name);
        }

        private static bool DerivationEquals(Grammar grammar, RuleDerivation derivation, List<string> referenceList)
        {
            if (referenceList.Count != derivation.Count) return false;
            for (int i = 0; i < derivation.Count; i++)
            {
                if (!referenceList[i].Equals(derivation[i].Name)) return false;
            }
            return true;
        }


        #endregion

        #region Individual components checking

        internal static void AssertRule(Rule? rule, string token, List<string> derivation)
        {
            Assert.NotNull(rule);
            Assert.NotNull(rule.Axiom);
            Assert.Equal(token, rule.Axiom.Name);
            Assert.Equal(derivation.Count, rule.Derivation.Count);
            for (int i = 0; i < derivation.Count; i++)
            {
                Assert.Equal(derivation[i], rule.Derivation[i].Name);
            }
        }

        internal static void AssertRegex(RegularExpression? regex, string token, string pattern)
        {
            Assert.NotNull(regex);
            Assert.NotNull(regex.Axiom);
            Assert.Equal(token, regex.Axiom.Name);
            Assert.Equal(pattern, regex.Pattern);
        }

        #endregion

    }
}
