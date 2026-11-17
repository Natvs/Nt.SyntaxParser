using Nt.Parser.Structures;
using Nt.Syntax.Structures;
using static Nt.Syntax.Exportation.ExportationMethods;

namespace Nt.Syntax.Exportation
{
    public enum ExportationMode
    {
        Original,
        Alphabetical,
        Grouped
    }

    public static class StringExporterExtensions
    {

        /// <summary>
        /// Export a collection of symbols to string with the specified exportation mode.
        /// </summary>
        /// <param name="symbols">Set of symbols to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representing this list of symbols</returns>
        public static string ToString(this ElementsSet<GrammarToken> symbols, ExportationMode mode)
        {
            var ordered_symbols = Reorder(symbols.ToList(), mode);
            return StringExporter.ToString(ordered_symbols);
        }

        /// <summary>
        /// Export a set of grammar rules to its string representation using the given nonterminals and exportation mode.
        /// </summary>
        /// <param name="rules">Set of grammar rules to convert to a string.</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representation of the grammar rules, formatted according to the specified nonterminals and exportation mode.</returns>
        public static string ToString(this ElementsSet<Rule> rules, ExportationMode mode)
        {
            var ordered_rules = Reorder(rules.ToList(), mode);
            return StringExporter.ToString(ordered_rules);
        }

        /// <summary>
        /// Export a set of regular expressions to a string representation according to the provided nonterminals and exportation mode.
        /// </summary>
        /// <param name="regexes">Set of regular expressions to convert.</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representation of the regular expressions, formatted according to the specified nonterminals and exportation mode.</returns>
        public static string ToString(this ElementsSet<RegularExpression> regexes, ExportationMode mode)
        {
            var ordered_regexes = Reorder(regexes.ToList(), mode);
            return StringExporter.ToString(ordered_regexes);
        }

        /// <summary>
        /// Export a grammar to string with the specified exportation mode.
        /// </summary>
        /// <param name="grammar">Grammar to export</param>
        /// <param name="mode">Exportation mode (original, sorted, etc.)</param>
        /// <returns>A string representing the grammar</returns>
        public static string ToString(this Grammar grammar, ExportationMode mode)
        {
            var terminals = Reorder([.. grammar.Terminals], mode);
            var nonterminals = Reorder([.. grammar.NonTerminals], mode);
            var rules = Reorder([.. grammar.Rules], mode);
            var regexes = Reorder([.. grammar.RegularExpressions], mode);
            return StringExporter.ToString(terminals, nonterminals, grammar.Axiom, rules, regexes);
        }

    }
}
