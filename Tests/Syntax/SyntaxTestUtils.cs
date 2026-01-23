using Nt.Parser.Structures;
using Nt.Syntax.Structures;

namespace Nt.Tests.Syntax
{
    public class SyntaxTestUtils
    {
        private static bool DerivationEquals(Grammar grammar, Derivation derivation, List<string> referenceList)
        {
            if (referenceList.Count != derivation.Count) return false;
            for (int i = 0; i < derivation.Count; i++)
            {
                if (!referenceList[i].Equals(derivation[i].Name)) return false;
            }
            return true;
        }

        #region Grammar checking
        internal static void AssertTokens(SymbolsList symbols, List<string> reference)
        {
            Assert.Equal(reference.Count, symbols.GetCount());
            for (int i = 0; i < symbols.GetCount(); i++)
            {
                Assert.Equal(reference[i], symbols.Get(i).Name);
            }
        }

        internal static void AssertRules(Grammar grammar, List<(string, List<string>)> referenceList)
        {
            Assert.Equal(referenceList.Count, grammar.Rules.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(grammar.Rules, rule =>
                    rule.Token != null
                    && rule.Token.Name.Equals(reference.Item1)
                    && DerivationEquals(grammar, rule.Derivation, reference.Item2));
            }
        }

        internal static void AssertRegex(Grammar grammar, List<(string, string)> referenceList)
        {
            Assert.Equal(referenceList.Count, grammar.RegularExpressions.Count);
            foreach (var reference in referenceList)
            {
                Assert.Contains(grammar.RegularExpressions, regex =>
                    regex.Token != null
                    && regex.Token.Name == reference.Item1
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
            Assert.True(grammar.NonTerminals.Contains(axiom));
            Assert.NotNull(grammar.Axiom);
            Assert.Equal(axiom, grammar.Axiom.Name);
        }

        #endregion

        #region Individual components checking

        internal static void AssertRule(Rule? rule, string token, List<string> derivation)
        {
            Assert.NotNull(rule);
            Assert.NotNull(rule.Token);
            Assert.Equal(token, rule.Token.Name);
            Assert.Equal(derivation.Count, rule.Derivation.Count);
            for (int i = 0; i < derivation.Count; i++)
            {
                Assert.Equal(derivation[i], rule.Derivation[i].Name);
            }
        }

        internal static void AssertRegex(RegularExpression? regex, string token, string pattern)
        {
            Assert.NotNull(regex);
            Assert.NotNull(regex.Token);
            Assert.Equal(token, regex.Token.Name);
            Assert.Equal(pattern, regex.Pattern);
        }

        #endregion

    }
}
