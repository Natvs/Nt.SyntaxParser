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
        /// <param name="symbols">List of symbols to export</param>
        /// <param name="mode">Mode to export the list of symbols</param>
        /// <returns>A string representing this list of symbols</returns>
        public static string ToString(this SymbolsList symbols, ExportationMode mode)
        {
            var ordered_symbols = Reorder(symbols.GetSymbols(), mode);
            return StringExporter.ToString(ordered_symbols);
        }

        /// <summary>
        /// Export a set of grammar rules to its string representation using the given nonterminals and exportation mode.
        /// </summary>
        /// <param name="rules">The set of grammar rules to convert to a string.</param>
        /// <param name="nonterminals">An ordered collection of nonterminal symbols that determines the output order of the rules.</param>
        /// <param name="mode">The exportation mode that specifies formatting or filtering options for the output.</param>
        /// <returns>A string representation of the grammar rules, formatted according to the specified nonterminals and exportation mode.</returns>
        public static string ToString(this RulesSet rules, ExportationMode mode)
        {
            var ordered_rules = Reorder(rules.ToList(), mode);
            return StringExporter.ToString(ordered_rules);
        }

        /// <summary>
        /// Export a set of regular expressions to a string representation according to the provided nonterminals and exportation mode.
        /// </summary>
        /// <param name="regexes">The set of regular expressions to convert.</param>
        /// <param name="nonterminals">An ordered collection of nonterminal symbols that determines the output order of the regular expressions.</param>
        /// <param name="mode">The exportation mode that specifies formatting or output rules for the conversion.</param>
        /// <returns>A string representation of the regular expressions, formatted according to the specified nonterminals and exportation mode.</returns>
        public static string ToString(this RegexSet regexes, ExportationMode mode)
        {
            var ordered_regexes = Reorder(regexes.ToList(), mode);
            return StringExporter.ToString(ordered_regexes);
        }

        /// <summary>
        /// Export a grammar to string with the specified exportation mode.
        /// </summary>
        /// <param name="grammar">Grammar to export</param>
        /// <param name="mode">Mode to export the grammar</param>
        /// <returns>A string representing the grammar</returns>
        public static string ToString(this Grammar grammar, ExportationMode mode)
        {
            var terminals = Reorder(grammar.Terminals.GetSymbols(), mode);
            var nonterminals = Reorder(grammar.NonTerminals.GetSymbols(), mode);
            var rules = Reorder(grammar.Rules.ToList(), mode);
            var regexes = Reorder(grammar.RegularExpressions.ToList(), mode);
            return StringExporter.ToString(terminals, nonterminals, grammar.Axiom, rules, regexes);
        }

    }
}
