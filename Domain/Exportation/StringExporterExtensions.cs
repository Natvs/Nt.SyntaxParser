using Nt.Parser.Structures;
using Nt.Parser.Symbols;
using Nt.Syntax.Structures;

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
        private static IEnumerable<ISymbol> Reorder(IEnumerable<ISymbol> symbols, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return symbols;
                case ExportationMode.Alphabetical:
                    return symbols.OrderBy(symbol => symbol.Name);
                case ExportationMode.Grouped:
                    var queue = new List<ISymbol>(symbols);
                    var ordered_symbols = new List<ISymbol>();
                    while (queue.Count > 0)
                    {
                        var current_symbol = queue[0];
                        ordered_symbols.Add(current_symbol);
                        var similar_symbols = queue.Where(symbol => symbol.Name.StartsWith(current_symbol.Name));
                        foreach (var symbol in similar_symbols)
                        {
                            ordered_symbols.Add(symbol);
                            queue.Remove(symbol);
                        }
                    }
                    return ordered_symbols;
            }
            return symbols;
        }

        private static IEnumerable<Rule> Reorder(IEnumerable<Rule> rules, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return rules;
                case ExportationMode.Alphabetical:
                    var queue = new List<Rule>(rules.OrderBy(rule => rule.Token?.Name));
                    var ordered_rules = new List<Rule>();
                    while (queue.Count > 0)
                    {
                        var current_rule = queue[0];
                        ordered_rules.Add(current_rule);
                        queue.RemoveAt(0);
                        var similar_rules = queue.Where(rule => rule.Token?.Name == current_rule.Token?.Name).OrderBy(rule => rule.ToString());
                        foreach (var rule in similar_rules)
                        {
                            ordered_rules.Add(rule);
                            queue.Remove(rule);
                        }
                    }
                    return ordered_rules;
                case ExportationMode.Grouped:
                    queue = [.. rules];
                    ordered_rules = [];
                    while (queue.Count > 0)
                    {
                        var current_rule = queue[0];
                        ordered_rules.Add(current_rule);
                        queue.RemoveAt(0);
                        if (current_rule.Token == null) continue;
                        var similar_rules = queue.Where(rule => rule.Token != null && rule.Token.Name.StartsWith(current_rule.Token.Name));
                        foreach (var rule in similar_rules)
                        {
                            ordered_rules.Add(rule);
                            queue.Remove(rule);
                        }
                    }
                    return ordered_rules;
            }
            return rules;
        }

        private static IEnumerable<RegularExpression> Reorder(IEnumerable<RegularExpression> regexes, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return regexes;
                case ExportationMode.Alphabetical:
                    var queue = new List<RegularExpression>(regexes.OrderBy(regex => regex.Token?.Name));
                    var ordered_regexes = new List<RegularExpression>();
                    while (queue.Count > 0)
                    {
                        var current_regex = queue[0];
                        ordered_regexes.Add(current_regex);
                        queue.RemoveAt(0);

                        var similar_regexes = queue.Where(regex => regex.Token?.Name == current_regex.Token?.Name).OrderBy(regex => regex.ToString());
                        foreach (var regex in similar_regexes)
                        {
                            ordered_regexes.Add(regex);
                            queue.Remove(regex);
                        }
                    }
                    return ordered_regexes;
                case ExportationMode.Grouped:
                    queue = [.. regexes];
                    ordered_regexes = [];
                    while (queue.Count > 0)
                    {
                        var current_regex = queue[0];
                        ordered_regexes.Add(current_regex);
                        queue.RemoveAt(0);

                        if (current_regex.Token == null) continue;
                        var similar_regexes = queue.Where(regex => regex.Token != null && regex.Token.Name.StartsWith(current_regex.Token.Name));
                        foreach (var regex in similar_regexes)
                        {
                            ordered_regexes.Add(regex);
                            queue.Remove(regex);
                        }
                    }
                    return ordered_regexes;
            }
            return regexes;
        }

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
            var ordered_rules = Reorder(rules, mode);
            return StringExporter.ToString(ordered_rules);
        }

        /// <summary>
        /// Export a set of regular expressions to a string representation according to the provided nonterminals and exportation mode.
        /// </summary>
        /// <param name="regexes">The set of regular expressions to convert.</param>
        /// <param name="nonterminals">An ordered collection of nonterminal symbols that determines the output order of the regular expressions.</param>
        /// <param name="mode">The exportation mode that specifies formatting or output rules for the conversion.</param>
        /// <returns>A string representation of the regular expressions, formatted according to the specified nonterminals and exportation mode.</returns>
        public static string ToString(this RegExpSet regexes, ExportationMode mode)
        {
            var ordered_regexes = Reorder(regexes, mode);
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
            var rules = Reorder(grammar.Rules, grammar.NonTerminals.GetSymbols(), mode);
            var regexes = Reorder(grammar.RegularExpressions, grammar.NonTerminals.GetSymbols(), mode);
            return StringExporter.ToString(terminals, nonterminals, grammar.Axiom, rules, regexes);
        }

    }
}
