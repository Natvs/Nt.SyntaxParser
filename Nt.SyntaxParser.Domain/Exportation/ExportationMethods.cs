using Nt.Parser.Symbols;
using Nt.Syntax.Structures;

namespace Nt.Syntax.Exportation
{
    public static class ExportationMethods
    {
        public static List<ISymbol> Reorder(List<ISymbol> symbols, ExportationMode mode)
        {
            switch (mode)
            {
                case ExportationMode.Original:
                    return symbols;
                case ExportationMode.Alphabetical:
                    return [.. symbols.OrderBy(symbol => symbol.Name)];
                case ExportationMode.Grouped:
                    var queue = new List<ISymbol>(symbols);
                    var ordered_symbols = new List<ISymbol>();
                    while (queue.Count > 0)
                    {
                        var current_symbol = queue[0];
                        var similar_symbols = queue.Where(symbol => symbol.Name.StartsWith(current_symbol.Name)).ToList();
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
                    var queue = new List<Rule>(rules.OrderBy(rule => rule.Token?.Name));
                    var ordered_rules = new List<Rule>();
                    while (queue.Count > 0)
                    {
                        var current_rule = queue[0];
                        ordered_rules.Add(current_rule);
                        queue.RemoveAt(0);
                        var similar_rules = queue.Where(rule => rule.Token?.Name == current_rule.Token?.Name).OrderBy(rule => rule.ToString()).ToList();
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
                        var similar_rules = queue.Where(rule => rule.Token != null && rule.Token.Name.StartsWith(current_rule.Token.Name)).ToList();
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
                    var queue = new List<RegularExpression>(regexes.OrderBy(regex => regex.Token?.Name));
                    var ordered_regexes = new List<RegularExpression>();
                    while (queue.Count > 0)
                    {
                        var current_regex = queue[0];
                        ordered_regexes.Add(current_regex);
                        queue.RemoveAt(0);

                        var similar_regexes = queue.Where(regex => regex.Token?.Name == current_regex.Token?.Name).OrderBy(regex => regex.ToString()).ToList();
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
                        var similar_regexes = queue.Where(regex => regex.Token != null && regex.Token.Name.StartsWith(current_regex.Token.Name)).ToList();
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
