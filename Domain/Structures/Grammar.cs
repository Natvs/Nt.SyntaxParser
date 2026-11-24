using Nt.Syntax.Builders;
using Nt.Syntax.Exportation;

namespace Nt.Syntax.Structures
{

    /// <summary>
    /// Represents datas of a language grammar
    /// </summary>
    public class Grammar()
    {

        /// <summary>
        /// Removes escape characters from the specified string, returning a new string with escape sequences processed.
        /// </summary>
        /// <param name="content">The input string from which escape characters should be removed.</param>
        /// <returns>A new string with escape characters removed from the input.</returns>
        internal string RemoveEscapeCharacter(string content)
        {
            string new_content = "";
            bool escape = false;
            foreach (var c in content)
            {
                if (c == EscapeCharacter && !escape)
                {
                    escape = true;
                    continue;
                }
                new_content += c;
                escape = false;
            }
            return new_content;
        }

        #region Public

        public char EscapeCharacter { get; private set; } = '\'';
        public ElementsSet<Terminal> Terminals { get; } = [];
        public ElementsSet<NonTerminal> NonTerminals { get; } = [];
        public NonTerminal? Axiom { get; internal set; } = null;
        public ElementsSet<Rule> Rules { get; } = [];
        public ElementsSet<RegularExpression> RegularExpressions { get; } = [];

        /// <summary>
        /// Sets the character used to escape special characters in parsing or formatting operations.
        /// </summary>
        /// <param name="c">The character to use as the escape character.</param>
        public void SetEscapeChar(char c)
        {
            EscapeCharacter = c;
        }

        /// <summary>
        /// Gets a string containing datas about the grammar
        /// </summary>
        /// <returns>A string representing this grammar</returns>
        public override string ToString()
        {
           return this.ToString(ExportationMode.Original);
        }

        public static Grammar operator + (Grammar grammar1, Grammar grammar2)
        {
            var joinedGrammar = new Grammar();
            var builder = joinedGrammar.GetBuilder();

            // Merge terminals
            foreach (var terminal in grammar1.Terminals)
            {
                builder.AddTerminal(terminal.Name, -1);
            }
            foreach (var terminal in grammar2.Terminals)
            {
                if (!joinedGrammar.Terminals.Contains((t) => t.Name == terminal.Name))
                {
                    builder.AddTerminal(terminal.Name, -1);
                }
            }

            // Merge non-terminals
            foreach (var nonTerminal in grammar1.NonTerminals)
            {
                builder.AddNonTerminal(nonTerminal.Name, -1);
            }
            foreach (var nonTerminal in grammar2.NonTerminals)
            {
                if (!joinedGrammar.NonTerminals.Contains((nt) => nt.Name == nonTerminal.Name))
                {
                    builder.AddNonTerminal(nonTerminal.Name, -1);
                }
            }

            // Merge rules
            foreach (var rule in grammar1.Rules)
            {
                if (rule.Axiom == null) continue;
                builder.AddRule(rule.Axiom.Name, rule.Axiom.Line, out var new_rule);
                foreach (var derivation in rule.Derivation)
                {
                    new_rule.GetBuilder().Add(derivation.Name, -1);
                }
            }
            foreach (var rule in grammar2.Rules)
            {
                if (rule.Axiom == null) continue;
                builder.AddRule(rule.Axiom.Name, rule.Axiom.Line, out var new_rule);
                foreach (var derivation in rule.Derivation)
                {
                    new_rule.GetBuilder().Add(derivation.Name, -1);
                }
            }

            // Merge regular expressions
            foreach (var regex in grammar1.RegularExpressions)
            {
                if (regex.Axiom == null) continue;
                builder.AddRegularExpression(regex.Axiom.Name, regex.Axiom.Line, out var new_regex);
                new_regex.GetBuilder().SetPattern(regex.Pattern);
            }
            foreach (var regex in grammar2.RegularExpressions)
            {
                if (regex.Axiom == null) continue;
                builder.AddRegularExpression(regex.Axiom.Name, regex.Axiom.Line, out var new_regex);
                new_regex.GetBuilder().SetPattern(regex.Pattern);
            }
            return joinedGrammar;
        }

        #endregion
    }
}
