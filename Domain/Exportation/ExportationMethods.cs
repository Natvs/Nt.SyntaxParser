using Nt.Syntax.Structures;

namespace Nt.Syntax.Exportation
{
    internal static class ExportationMethods
    {
        public static List<GrammarToken> Reorder(List<GrammarToken> symbols, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return symbols;
                case ExportationMode.Alphabetical:
                    return [.. symbols.OrderBy(symbol => symbol.Name)];
                case ExportationMode.Grouped:
                    var queue = new List<GrammarToken>(symbols);
                    var ordered_symbols = new List<GrammarToken>();
                    while (queue.Count > 0)
                    {
                        var current_symbol = queue[0];
                        var similar_symbols = queue.Where(symbol => symbol.Name.StartsWith(current_symbol.Name, StringComparison.Ordinal)).ToList();
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

        public static List<Rule> Reorder(List<Rule> rules, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return rules;
                case ExportationMode.Alphabetical:
                    var queue = new List<Rule>(rules.OrderBy(rule => rule.Axiom?.Name));
                    var ordered_rules = new List<Rule>();
                    while (queue.Count > 0)
                    {
                        var current_rule = queue[0];
                        ordered_rules.Add(current_rule);
                        queue.RemoveAt(0);
                        var similar_rules = queue.Where(rule => rule.Axiom?.Name == current_rule.Axiom?.Name).OrderBy(rule => rule.ToString()).ToList();
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
                        if (current_rule.Axiom == null) continue;
                        var similar_rules = queue.Where(rule => rule.Axiom != null && rule.Axiom.Name.StartsWith(current_rule.Axiom.Name, StringComparison.Ordinal)).ToList();
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

        public static List<RegularExpression> Reorder(List<RegularExpression> regexes, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return regexes;
                case ExportationMode.Alphabetical:
                    var queue = new List<RegularExpression>(regexes.OrderBy(regex => regex.Axiom?.Name));
                    var ordered_regexes = new List<RegularExpression>();
                    while (queue.Count > 0)
                    {
                        var current_regex = queue[0];
                        ordered_regexes.Add(current_regex);
                        queue.RemoveAt(0);

                        var similar_regexes = queue.Where(regex => regex.Axiom?.Name == current_regex.Axiom?.Name).OrderBy(regex => regex.ToString()).ToList();
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

                        if (current_regex.Axiom == null) continue;
                        var similar_regexes = queue.Where(regex => regex.Axiom != null && regex.Axiom.Name.StartsWith(current_regex.Axiom.Name, StringComparison.Ordinal)).ToList();
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

    }
}
